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
using BookSharing.Infrastructure.Interface;
using MediatR;
using Refit;

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
            services.AddTransient<IBookService, BookApiRetriver>();
         
            services.AddRefitClient<IBookApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(/*"http://openlibrary.org"*/"https://www.googleapis.com"));

            services.AddMediatR(typeof(GetAllBooksQueryHandler));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookSharing.API", Version = "v1" });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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
    }
}
