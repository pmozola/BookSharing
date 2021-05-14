using Microsoft.Extensions.DependencyInjection;
using BookSharing.Application.Interface;
using Microsoft.AspNetCore.SignalR;
using BookSharing.API.SingnalRHubs;

namespace BookSharing.API
{
    public static class ConfigureBookShareSignalR
    {
        public static IServiceCollection AddBookSharingSignalR(this IServiceCollection services)
        {
            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, BookSharingSignalRUserProvider>();
            services.AddTransient<IWantedBookRealTimeNotifcation, WantedBookRealTimeNotifcation>();

            return services;
        }
    }
}
