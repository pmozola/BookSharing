using System.Threading;
using System.Threading.Tasks;

namespace BookSharing.Domain.BookAggregate
{
    public interface IBookRepository
    {
        Task<bool> IsExistAsync(long isbn, CancellationToken cancellationToken);
        Task AddAsync(Book book, CancellationToken cancellationToken);
        Task<Book> GetAsync(long isbn, CancellationToken cancellationToken);
        Task<Book> GetAsyncByTitle(string title, CancellationToken cancellationToken);
       
    }
}
