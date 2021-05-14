using BookSharing.API.SignalRHubs;
using BookSharing.Application.Interface;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace BookSharing.API.Config
{
    public static class ConfigureBookShareSignalR
    {
        public static IServiceCollection AddBookSharingSignalR(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, BookSharingSignalRUserProvider>();
            services.AddTransient<IWantedBookRealTimeNotification, WantedBookRealTimeNotification>();

            return services;
        }
    }
}
