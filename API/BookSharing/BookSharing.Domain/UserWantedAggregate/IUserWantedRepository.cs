using System.Threading;
using System.Threading.Tasks;

namespace BookSharing.Domain.UserWantedAggregate
{
    public interface IUserWantedRepository
    {
        Task AddAsync(UserWanted entity, CancellationToken cancelationToken);
        Task Delete(UserWanted entity, CancellationToken cancellationToken);
        Task<UserWanted> GetForUser(long iSBN, int v, CancellationToken cancellationToken);
    }
}
