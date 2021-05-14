using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BookSharing.Infrastructure;
using MediatR;
using BookSharing.Application.QueryHandlers.UserLibrary;
using BookSharing.API.Infrastructure;
using BookSharing.API.BackgroundTasks;
using BookSharing.API.Config;
using BookSharing.API.SignalRHubs;
using BookSharing.Auth.Application;
using BookSharing.Auth.Application.Config;
using BookSharing.Application.Ioc;
using BookSharing.Auth.Application.CommandHandlers;
using BookSharing.Application.Interface;
using BookSharing.Auth.Application.UserSeed;

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
            var authSettings = GetAuthSettings();

            services.AddCors();
            services.AddControllers();

            services
                .AddDbContext<BookSharingDbContext>(x => x.UseInMemoryDatabase(databaseName: "BookSharingDatabase"))
                .AddScoped<BookSharingDbContext>();

            services.AddBookShareIdentity();

            services.AddBookSharingSignalR();

            services.Configure<ExternalApiUrls>(options => Configuration.GetSection("ExternalApiUrls").Bind(options));
            services.Configure<AuthSettings>(options => Configuration.GetSection("Auth").Bind(options));
            services.Configure<TestUsersConfig>(options => Configuration.GetSection("TestUsers").Bind(options));

            services.AddBookShareAuthorization(authSettings);

            services.AddExternalApiClients(Configuration.GetSection("ExternalApiUrls").Get<ExternalApiUrls>());
            services.AddHostedService<OutboxMessageBackgroundTask>();

            services.AddBookSharingServices();
            services.AddMediatR(typeof(GetAllUserBooksQuery));
            services.AddMediatR(typeof(LoginUserCommand));
            services.AddHttpContextAccessor();
            services.AddTransient<IUserContext, HttpContextUser>();

            services.AddBookSharingSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseBookShareSwagger(IsSwaggerEnabled(env));

            app.UseRouting();

            app.UseCors(x => x
               .AllowAnyMethod()
               .AllowAnyHeader()
               .SetIsOriginAllowed(_ => true)
               .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
                endpoints.MapHub<WantedBooksHub>("/wantedBookHub");
            });
        }

        private bool IsSwaggerEnabled(IWebHostEnvironment env)
        {
            return env.IsDevelopment() || Configuration.GetValue<bool>("EnableSwaggerInProduction");
        }

        private AuthSettings GetAuthSettings()
        {
            return Configuration.GetSection("Auth").Get<AuthSettings>();
        }
    }
}
