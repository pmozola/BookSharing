using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using BookSharing.Application.QueryHandlers.Books;
using BookSharing.Infrastructure;
using BookSharing.Infrastructure.BookApi;
using MediatR;
using BookSharing.Application.QueryHandlers.UserLibrary;
using BookSharing.Infrastructure.Repositories;
using BookSharing.Domain.UserBookAggregate;
using BookSharing.Application.Interface;
using BookSharing.API.Infrastructure;
using Refit;
using BookSharing.Domain.BookAggregate;
using BookSharing.Infrastructure.BookApi.Google;
using BookSharing.API.BackgroundTasks;

namespace BookSharing.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            services.AddDbContext<BookSharingDbContext>(x => x.UseInMemoryDatabase(databaseName: "BookSharingDatabase"));
            services.AddScoped<BookSharingDbContext>();
            services.AddTransient<IUserBookRepository, UserBookRepository>();
            services.AddTransient<IBookRepository, BookRepository>();

            services.AddTransient<IUserContext, FakeHttpUserContext>();
            services.AddTransient<IExternalBookApiProvider, GoogleBookProvider>();

            services.AddHostedService<OutboxMessageBackgroundTask>();

            services.AddRefitClient<IGoogleBookApiClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration.GetValue<string>("GoogleBookApi")));

            services.AddMediatR(typeof(GetAllUserBooksQuery));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookSharing.API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (IsSwaggerEnabled(env))
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookSharing.API v1"));
            }

            app.UseRouting();

            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(origin => true)
               .AllowCredentials());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private bool IsSwaggerEnabled(IWebHostEnvironment env)
        {
            return env.IsDevelopment() || Configuration.GetValue<bool>("EnabbleSwaggerInProduction");
        }
    }
}
