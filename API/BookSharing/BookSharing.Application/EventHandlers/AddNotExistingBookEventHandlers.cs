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
        private readonly IExternalBookApiProvider _bookProvider;
        private readonly IBookRepository _repository;
        private readonly ILogger<AddNotExistingBookEventHandlers> _logger;

        public AddNotExistingBookEventHandlers(
            IExternalBookApiProvider bookProvider,
            IBookRepository repository,
            ILogger<AddNotExistingBookEventHandlers> logger)
        {
            _bookProvider = bookProvider;
            _repository = repository;
            _logger = logger;
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
            if (await _repository.IsExistAsync(isbn, cancellationToken))
            {
                return;
            }

            var book = await _bookProvider.GetBook(isbn);

            if (book == null)
            {
                _logger.LogError($"Cannot find book with isbn: {isbn} added by user: {userId}");
                
                return;
            }

            await _repository.AddAsync(new Book(book.Isbn, book.Title, string.Join(", ", book.Autor), book.Year, book.Description, book.ImageUrl), cancellationToken);
        }
    }
}
