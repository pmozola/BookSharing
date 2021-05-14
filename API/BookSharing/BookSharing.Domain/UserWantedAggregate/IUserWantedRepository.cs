using System.Threading;
using System.Threading.Tasks;

namespace BookSharing.Domain.UserWantedAggregate
{
    public interface IUserWantedRepository
    {
        Task AddAsync(UserWanted entity, CancellationToken cancellationToken);
        Task Delete(UserWanted entity, CancellationToken cancellationToken);
        Task<UserWanted> GetForUser(long isbn, int userId, CancellationToken cancellationToken);
    }
}
