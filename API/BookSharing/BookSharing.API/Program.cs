using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

using BookSharing.Auth.Application.UserSeed;
using BookSharing.Infrastructure.SeedData;

namespace BookSharing.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase()
                .MigrateUser()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
