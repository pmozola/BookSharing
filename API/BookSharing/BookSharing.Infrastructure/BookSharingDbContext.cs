using Microsoft.EntityFrameworkCore;

using BookSharing.Domain.BookAggregate;

namespace BookSharing.Infrastructure
{
    public class BookSharingDbContext : DbContext
    {
        public BookSharingDbContext(DbContextOptions<BookSharingDbContext> options)
        : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
    }
}
