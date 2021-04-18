using BookSharing.Application.Interface;

namespace BookSharing.API.Infrastructure
{
    public class HttpUserContext : IUserContext
    {
        // TODO mocked until authorization is implemented
        public int GetUserId() => 1;
    }
}
