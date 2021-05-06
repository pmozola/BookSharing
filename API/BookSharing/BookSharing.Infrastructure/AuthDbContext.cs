using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookSharing.Infrastructure
{
    public class AuthDbContext : IdentityDbContext<AppUser, AppIdentityRole, int>
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        { }
    }

    public class AppUser : IdentityUser<int> { }
    public class AppIdentityRole : IdentityRole<int> { }
}
