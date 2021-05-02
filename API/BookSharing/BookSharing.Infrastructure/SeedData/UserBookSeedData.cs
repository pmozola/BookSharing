using BookSharing.Domain.UserBookAggregate;
using System.Threading.Tasks;

namespace BookSharing.Infrastructure.SeedData
{
    public static class UserBooksSeedData
    {
        public static async Task Seed(BookSharingDbContext dbContext)
        {
            dbContext.UserBooks.AddRange(new[]
            {
                new UserBook(1, 1111111111111, 1),
                new UserBook(1, 2222222222222, 2),
                new UserBook(1, 3333333333333, 3),
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
