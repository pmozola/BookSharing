using System;
using BookSharing.Infrastructure.BookApi.Google;
using BookSharing.Infrastructure.BookApi.OpenLibrary;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace BookSharing.API.Config
{
    public static class ConfigureExternalApiClients
    {
        public static IServiceCollection AddExternalApiClients(this IServiceCollection services, ExternalApiUrls externalApiClients)
        {
            services.
                AddRefitClient<IGoogleBookApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(externalApiClients.GoogleBooks));

            services.AddRefitClient<IOpenLibraryApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(externalApiClients.OpenBookLibrary));

            return services;
        }
    }
}
