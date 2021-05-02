using System.Threading;
using System.Threading.Tasks;
using BookSharing.Domain.BookAggregate;
using BookSharing.Domain.UserBookAggregate;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BookSharing.Application.EventHandlers
{
    public class BookAddedToUserLibraryEventHandler : INotificationHandler<BookAddedToUserLibraryEvent>
    {
        private readonly IExternalBookApiProvider bookProvider;
        private readonly IBookRepository repository;
        private readonly ILogger<BookAddedToUserLibraryEventHandler> logger;

        public BookAddedToUserLibraryEventHandler(
            IExternalBookApiProvider bookProvider,
            IBookRepository repository,
            ILogger<BookAddedToUserLibraryEventHandler> logger)
        {
            this.bookProvider = bookProvider;
            this.repository = repository;
            this.logger = logger;
        }

        public async Task Handle(BookAddedToUserLibraryEvent notification, CancellationToken cancellationToken)
        {
            if (await repository.IsExistAsync(notification.Isbn, cancellationToken))
            {
                return;
            }

            var book = await this.bookProvider.GetBook(notification.Isbn);

            if (book == null)
            {
                logger.LogError($"Cannot find book with isn: {notification.Isbn} added by user: {notification.UserId}");
            }

            await repository.AddAsync(new Book(book.Isbn, book.Title, string.Join(", ", book.Autor), book.Year, book.Description, book.ImageUrl), cancellationToken);
        }
    }
}
