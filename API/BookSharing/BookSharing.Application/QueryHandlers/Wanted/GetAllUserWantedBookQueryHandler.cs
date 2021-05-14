using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookSharing.Application.Interface;
using BookSharing.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookSharing.Application.QueryHandlers.Wanted
{
    public class GetAllUserWantedBookQueryHandler : IRequestHandler<GetAllUserWantedBookQuery, UserWantedBookResource[]>
    {
        private readonly BookSharingDbContext _repository;
        private readonly IUserContext _userContext;

        public GetAllUserWantedBookQueryHandler(BookSharingDbContext repository, IUserContext userContext)
        {
            _repository = repository;
            _userContext = userContext;
        }

        public Task<UserWantedBookResource[]> Handle(GetAllUserWantedBookQuery request, CancellationToken cancellationToken)
        {
            return _repository.UserWantedBooks
                .Where(x => x.UserId == _userContext.GetUserId())
                .Join(
                    _repository.Books,
                    userWantedBooks => userWantedBooks.ISBN,
                    books => books.ISBN,
                    (userWantedBook, book) => new UserWantedBookResource(userWantedBook.Id, book.Title, book.ImageUrl))
                .ToArrayAsync(cancellationToken);
        }
    }

    public record GetAllUserWantedBookQuery : IRequest<UserWantedBookResource[]>;
    public record UserWantedBookResource(int Id, string Title, string ImageUrl);
}