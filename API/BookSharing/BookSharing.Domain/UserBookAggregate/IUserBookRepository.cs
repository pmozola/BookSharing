using System.Threading.Tasks;

namespace BookSharing.Domain.UserBookAggregate
{
    public interface IUserBookRepository
    {
        Task AddAsync(UserBook book);
        Task<UserBook> GetAsync(int bookId, int userId);
        Task UpdateAsync(UserBook book);
    }
}
