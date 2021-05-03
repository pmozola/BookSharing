using BookSharing.Domain.Base;

namespace BookSharing.Domain.UserWantedAggregate
{
    public class UserWanted : Entity, IAggregateRoot
    {
        public UserWanted(long isbn, int userId)
        {
            ISBN = isbn;
            UserId = userId;

            this.AddDomainEvent(new UserWantedBookAddedEvent(this.UserId, this.ISBN));
        }

        private UserWanted() { }

        public long ISBN { get; private set; }
        public int UserId { get; private set; }
    }
}
