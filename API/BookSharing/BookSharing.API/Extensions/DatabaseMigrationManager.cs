﻿using System;
using BookSharing.Infrastructure;
using BookSharing.Infrastructure.SeedData;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookSharing.API.Extensions
{
    public static class DatabaseMigrationManager
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<BookSharingDbContext>>();
                using var appContext = scope.ServiceProvider.GetRequiredService<BookSharingDbContext>();
                
                try
                {
                    BooksSeedData.Seed(appContext).Wait();
                }
                catch (Exception ex)
                {
                    logger.LogError("Exception on database migration", ex);
                    throw;
                }
            }

            return host;
        }
    }
}
