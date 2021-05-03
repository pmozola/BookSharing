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
                new Book(1111111111111, "Wzorce projektowe. Elementy oprogramowania obiektowego wielokrotnego użytku", "Erich Gamma", 2030, "Book about design patterns", "https://blog.artmetic.pl/wp-content/uploads/2015/11/Wzorce-Projektowe-794x1024.jpg"),
                new Book(2222222222222, "Domain-Driven Design. Zapanuj nad złożonym systemem informatycznym", "Eric Evans", 2020, "Book about Domain-Driven Design", "https://www.bbc.co.uk/staticarchive/a0ef83ea6587a3161de39009344958289a6f7353.jpg"),
                new Book(3333333333333, "Sapiens. Od zwierząt do bogów", "Yuval Noah Harari", 2019, "Book about how man came into being", "https://cdn-lubimyczytac.pl/upload/books/4943000/4943173/882012-352x500.jpg"),
            });

            await dbContext.SaveChangesAsync();
        }
    }
}