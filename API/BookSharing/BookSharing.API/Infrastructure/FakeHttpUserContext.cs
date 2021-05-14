using BookSharing.Application.Interface;

namespace BookSharing.API.Infrastructure
{
    public class FakeHttpUserContext : IUserContext
    {
        public int GetUserId() => 1;
    }
}
