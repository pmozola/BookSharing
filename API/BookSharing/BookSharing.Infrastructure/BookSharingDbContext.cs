using Microsoft.EntityFrameworkCore;

using BookSharing.Domain.BookAggregate;
using BookSharing.Domain.UserBookAggregate;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using BookSharing.Domain.Base;
using System;
using BookSharing.Infrastructure.Models;
using Newtonsoft.Json;
using BookSharing.Domain.UserWantedAggregate;

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
        public DbSet<UserWanted> UserWantedBooks { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var domainEntities = ChangeTracker
             .Entries<Entity>()
             .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            foreach (var domainEvent in domainEvents)
            {
                var outboxMessage = new OutboxMessage(
                    occurredOn: DateTime.UtcNow,
                    type: domainEvent.GetType().FullName,
                    data: JsonConvert.SerializeObject(domainEvent, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }));

                OutboxMessages.Add(outboxMessage);
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
