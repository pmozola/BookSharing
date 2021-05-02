using System.Linq;
using System.Threading.Tasks;

using BookSharing.Domain.UserBookAggregate;
using Microsoft.EntityFrameworkCore;

namespace BookSharing.Infrastructure.Repositories
{
    public class UserBookRepository : IUserBookRepository
    {
        private BookSharingDbContext _dbContext;

        public UserBookRepository(BookSharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(UserBook book)
        {
            _dbContext.Add(book);

            await _dbContext.SaveChangesAsync();
        }

        public Task<UserBook> GetAsync(int bookId, int userId)
        {
            return _dbContext.UserBooks
                .Where(x => x.UserId == userId)
                .Where(x => x.Id == bookId)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(UserBook book)
        {
            _dbContext.Update(book);

            await _dbContext.SaveChangesAsync();
        }
    }
}
