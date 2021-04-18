using System.Threading.Tasks;

using BookSharing.Domain.BookAggregate;

namespace BookSharing.Infrastructure.SeedData
{
    public static class BooksSeedData
    {
        public static async Task Seed(BookSharingDbContext dbContext)
        {
            dbContext.Books.AddRange(new[]
            {
                new Book(ISBN: "9788324626625", Title: "Wzorce projektowe. Elementy oprogramowania obiektowego wielokrotnego użytku","1"),
                new Book(ISBN: "9788328305250", Title: "Domain-Driven Design. Zapanuj nad złożonym systemem informatycznym","1"),
                new Book(ISBN: "9788308068144", Title: "Sapiens. Od zwierząt do bogów","1"),
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
