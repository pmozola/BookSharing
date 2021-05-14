using BookSharing.Application.Interface;
using Microsoft.AspNetCore.SignalR;

namespace BookSharing.API.SignalRHubs
{
    public class BookSharingSignalRUserProvider : IUserIdProvider
    {
        private readonly IUserContext _userContext;
        public BookSharingSignalRUserProvider(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public string GetUserId(HubConnectionContext connection)
        {
            return _userContext.GetUserId().ToString();
        }
    }
}
