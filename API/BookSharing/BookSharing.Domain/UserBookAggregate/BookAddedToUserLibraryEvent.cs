using MediatR;

namespace BookSharing.Domain.UserBookAggregate
{
    public record BookAddedToUserLibraryEvent(long Isbn, int UserId) : INotification;
}
