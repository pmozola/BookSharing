using BookSharing.Domain.BookAggregate;
using BookSharing.Domain.UserBookAggregate;
using BookSharing.Domain.UserWantedAggregate;
using BookSharing.Infrastructure.BookApi;
using BookSharing.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BookSharing.Application.Ioc
{
    public static class BookSharingIoC
    {
        public static IServiceCollection AddBookSharingServices(this IServiceCollection services)
        {
            //services.AddTransient<IExternalBookApiProvider, GoogleBookProvider>();
            services.AddTransient<IUserBookRepository, UserBookRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IUserWantedRepository, UserWantedRepository>();
            services.AddTransient<IExternalBookApiProvider, OpenLibraryProvider>();
           
            return services;
        }
    }
}
