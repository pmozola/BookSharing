using Microsoft.EntityFrameworkCore;

using BookSharing.Domain.BookAggregate;
using BookSharing.Domain.UserBookAggregate;

namespace BookSharing.Infrastructure
{
    public class BookSharingDbContext : DbContext
    {
        public BookSharingDbContext(DbContextOptions<BookSharingDbContext> options)
        : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
    }
}
