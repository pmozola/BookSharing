using System;
using System.Collections.Generic;
using System.Linq;
using BookSharing.Auth.Application.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BookSharing.Auth.Application.UserSeed
{
    public static class UserMigrationManager
    {
        public static IHost MigrateUser(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                using var authContext = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<UserManager<AppUser>>>();
                var testUsersConfig = scope.ServiceProvider.GetRequiredService<IOptions<TestUsersConfig>>().Value;

                try
                {
                    foreach (var userToSeed in testUsersConfig.Users)
                    {
                        if (!authContext.Users.Any(u => u.UserName == userToSeed.UserName))
                        {
                            userManager.CreateAsync(new AppUser
                            {
                                UserName = userToSeed.UserName,
                                Email = userToSeed.Email
                            }, testUsersConfig.Password).Wait();
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
    }

    public class TestUsersConfig
    {
        public string Password { get; set; }
        public List<TestUser> Users { get; set; }
    }

    public class TestUser{
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
