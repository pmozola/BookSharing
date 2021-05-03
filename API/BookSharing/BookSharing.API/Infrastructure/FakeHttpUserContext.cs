using BookSharing.Application.Interface;

namespace BookSharing.API.Infrastructure
{
    public class FakeHttpUserContext : IUserContext
    {
        // TODO mocked until authorization is implemented
        public int GetUserId() => 1;
    }
}
