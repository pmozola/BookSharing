using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BookSharing.Domain.BookAggregate;
using Microsoft.EntityFrameworkCore;

namespace BookSharing.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookSharingDbContext _dbContext;

        public BookRepository(BookSharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Book book, CancellationToken cancellationToken)
        {
            _dbContext.Books.Add(book);

            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<Book> GetAsync(long isbn, CancellationToken cancellationToken)
        {
            return _dbContext.Books.Where(x => x.ISBN == isbn).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<bool> IsExistAsync(long isbn, CancellationToken cancellationToken)
        {
            return _dbContext.Books.Where(x => x.ISBN == isbn).AnyAsync(cancellationToken);
        }
    }
}
