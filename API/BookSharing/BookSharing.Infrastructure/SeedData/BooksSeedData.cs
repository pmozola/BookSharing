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
                new Book(ISBN: "9788324626625", Title: "Wzorce projektowe. Elementy oprogramowania obiektowego wielokrotnego użytku", Author:"Erich Gamma", Year:"1994-10-21", Description:"Book about design patterns" ),
                new Book(ISBN: "9788328305250", Title: "Domain-Driven Design. Zapanuj nad złożonym systemem informatycznym", Author:"Eric Evans", Year:"2015-07-06", Description:"Book about Domain-Driven Design"),
                new Book(ISBN: "9788308068144", Title: "Sapiens. Od zwierząt do bogów", Author:"Yuval Noah Harari", Year:"2018-11-14", Description:"Book about how man came into being"),
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
