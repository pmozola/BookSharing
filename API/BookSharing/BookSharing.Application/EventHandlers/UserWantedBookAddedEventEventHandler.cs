using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookSharing.Application.Interface;
using BookSharing.Domain.UserBookAggregate;
using BookSharing.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BookSharing.Application.EventHandlers
{
    public class BookAddedToUserLibraryEventHandler : INotificationHandler<BookAddedToUserLibraryEvent>
    {
        private readonly ILogger<BookAddedToUserLibraryEventHandler> _logger;
        private readonly BookSharingDbContext _dbContext;
        private readonly IWantedBookRealTimeNotifcation _realTimeNotification;

        public BookAddedToUserLibraryEventHandler(
            ILogger<BookAddedToUserLibraryEventHandler> logger,
            BookSharingDbContext dbContext,
            IWantedBookRealTimeNotifcation realTimeNotification)
        {
            _logger = logger;
            _dbContext = dbContext;
            _realTimeNotification = realTimeNotification;
        }
        public async Task Handle(BookAddedToUserLibraryEvent notification, CancellationToken cancellationToken)
        {
            var wantedBooks = await _dbContext.UserWantedBooks
                  .Where(x => x.ISBN == notification.Isbn)
                  .Where(x => x.UserId == notification.UserId)
                 .Join(
                    _dbContext.Books,
                    userBook => userBook.ISBN,
                    books => books.ISBN,
                    (userBook, book) => new { userBook.UserId, userBook.ISBN, book.Title })
                 .ToArrayAsync(cancellationToken);

            foreach (var wantedBook in wantedBooks)
            {
                _logger.LogInformation($"Notification for user { wantedBook.UserId}: book {wantedBook.ISBN} is now avabile for book sharing");
                await _realTimeNotification.SendMessage(wantedBook.UserId, wantedBook.ISBN.ToString(), wantedBook.Title);
            }
        }
    }
}
