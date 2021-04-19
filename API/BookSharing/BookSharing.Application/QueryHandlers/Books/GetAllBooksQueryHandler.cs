using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BookSharing.Infrastructure;
using MediatR;

namespace BookSharing.Application.QueryHandlers.Books
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, IList<BookResource>>
    {
        private readonly BookSharingDbContext _dbContext;

        public GetAllBooksQueryHandler(BookSharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IList<BookResource>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _dbContext.Books
                .Select(book => new BookResource(book.ISBN, book.Title, 2020))
                .ToListAsync(cancellationToken);

            return books;
        }
    }

    public record GetAllBooksQuery: IRequest<IList<BookResource>>;

    public record BookResource(string ISBN, string Title, int Year);
}
