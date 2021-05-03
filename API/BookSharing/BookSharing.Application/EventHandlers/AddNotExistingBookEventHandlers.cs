using System.Threading;
using System.Threading.Tasks;
using BookSharing.Domain.BookAggregate;
using BookSharing.Domain.UserBookAggregate;
using BookSharing.Domain.UserWantedAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookSharing.Application.EventHandlers
{
    public class AddNotExistingBookEventHandlers :
        INotificationHandler<BookAddedToUserLibraryEvent>,
        INotificationHandler<UserWantedBookAddedEvent>
    {
        private readonly IExternalBookApiProvider bookProvider;
        private readonly IBookRepository repository;
        private readonly ILogger<AddNotExistingBookEventHandlers> logger;

        public AddNotExistingBookEventHandlers(
            IExternalBookApiProvider bookProvider,
            IBookRepository repository,
            ILogger<AddNotExistingBookEventHandlers> logger)
        {
            this.bookProvider = bookProvider;
            this.repository = repository;
            this.logger = logger;
        }

        public async Task Handle(BookAddedToUserLibraryEvent notification, CancellationToken cancellationToken)
        {
            await AddNotExistingBook(notification.UserId, notification.Isbn, cancellationToken);
        }

        public async Task Handle(UserWantedBookAddedEvent notification, CancellationToken cancellationToken)
        {
            await AddNotExistingBook(notification.UserId, notification.ISBN, cancellationToken);
        }

        public async Task AddNotExistingBook(int userId, long isbn, CancellationToken cancellationToken)
        {
            if (await repository.IsExistAsync(isbn, cancellationToken))
            {
                return;
            }

            var book = await this.bookProvider.GetBook(isbn);

            if (book == null)
            {
                logger.LogError($"Cannot find book with isbn: {isbn} added by user: {userId}");
                
                return;
            }

            await repository.AddAsync(new Book(book.Isbn, book.Title, string.Join(", ", book.Autor), book.Year, book.Description, book.ImageUrl), cancellationToken);
        }
    }
}
