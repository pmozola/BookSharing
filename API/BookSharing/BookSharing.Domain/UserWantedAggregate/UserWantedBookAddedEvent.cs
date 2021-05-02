using MediatR;

namespace BookSharing.Domain.UserWantedAggregate
{
    public record UserWantedBookAddedEvent(int UserId, long ISBN) : INotification;
}
