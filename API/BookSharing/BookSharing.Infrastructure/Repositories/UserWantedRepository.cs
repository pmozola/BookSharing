using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using BookSharing.Domain.UserWantedAggregate;
using Microsoft.EntityFrameworkCore;

namespace BookSharing.Infrastructure.Repositories
{
    public class UserWantedRepository : IUserWantedRepository
    {
        private readonly BookSharingDbContext _dbContext;

        public UserWantedRepository(BookSharingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(UserWanted entity, CancellationToken cancellationToken)
        {
            await _dbContext.UserWantedBooks.AddAsync(entity, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Delete(UserWanted entity, CancellationToken cancellationToken)
        {
            _dbContext.UserWantedBooks.Remove(entity);

            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<UserWanted> GetForUser(long isbn, int userId, CancellationToken cancellationToken)
        {
            return _dbContext.UserWantedBooks
               .Where(x => x.ISBN == isbn)
               .Where(x => x.UserId == userId)
               .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
