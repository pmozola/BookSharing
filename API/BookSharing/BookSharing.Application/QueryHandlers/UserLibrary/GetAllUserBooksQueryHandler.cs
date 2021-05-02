using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookSharing.Application.Interface;
using BookSharing.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookSharing.Application.QueryHandlers.UserLibrary
{
    public class GetAllUserBooksQueryHandler : IRequestHandler<GetAllUserBooksQuery, UserBookShortInformation[]>
    {
        private readonly IUserContext _userContext;
        private readonly BookSharingDbContext _dbContext;

        public GetAllUserBooksQueryHandler(IUserContext userContext, BookSharingDbContext dbContext)
        {
            _userContext = userContext;
            _dbContext = dbContext;
        }
        public Task<UserBookShortInformation[]> Handle(GetAllUserBooksQuery request, CancellationToken cancellationToken)
        {
            return _dbContext.UserBooks
                .Where(x => x.UserId == _userContext.GetUserId())
                .Where(x => !x.WasGiven)
                .Join(
                    _dbContext.Books,
                    userBook => userBook.ISBN,
                    books => books.ISBN,
                    (userBook, book) => new UserBookShortInformation(userBook.Id, book.Title, book.ImageUrl, false))
                .ToArrayAsync(cancellationToken);
        }
    }

    public record GetAllUserBooksQuery() : IRequest<UserBookShortInformation[]>;
    public record UserBookShortInformation(int Id, string Title, string ImageUrl, bool IsAnyConversation);
}