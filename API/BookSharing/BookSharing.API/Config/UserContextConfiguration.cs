using BookSharing.API.Infrastructure;
using BookSharing.Application.Interface;
using BookSharing.Auth.Application;
using Microsoft.Extensions.DependencyInjection;

namespace BookSharing.API.Config
{
    public static class UserContextConfiguration
    {
        public  static IServiceCollection ConfigureUserContext( this IServiceCollection services, AuthSettings authSettings)
        {
            if (authSettings.IsEnabled)
            {
                services.AddTransient<IUserContext, HttpContextUser>();
            }
            else
            {
                services.AddTransient<IUserContext, FakeHttpUserContext>();
            }

            return services;
        }
    }
}
