using System;

using Microsoft.Extensions.DependencyInjection;
using Refit;
using BookSharing.Infrastructure.BookApi.Google;
using BookSharing.Infrastructure.BookApi.OpenLibrary;

namespace BookSharing.API
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
