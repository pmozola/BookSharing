using System;
using System.Collections.Generic;
using System.Linq;
using BookSharing.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookSharing.API.Extensions
{
    public static class UserMigrationManager
    {
        private const string password = "BooksSharing123";
        public static IHost MigrateUser(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                using var authContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<UserManager<AppUser>>>();

                try
                {
                    foreach (var userToSeed in GetDefaultUsers())
                    {
                        if (!authContext.Users.Any(u => u.UserName == userToSeed.Username))
                        {
                            userManager.CreateAsync(new AppUser
                            {
                                UserName = userToSeed.Username,
                                Email = userToSeed.Email
                            }, password).Wait();
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError("Exception on user migration", ex);
                    throw;
                }
            }
            return host;
        }

        private static IEnumerable<UserToSeed> GetDefaultUsers()
        {
            yield return new UserToSeed("pmozola", "pmozola@booksharingtest.com");
            yield return new UserToSeed("akasperiewicz", "akasperiewicz@booksharingtest.com");
        }

        private record UserToSeed(string Username, string Email);
    }
}
