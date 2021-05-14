using System.Text;
using BookSharing.Auth.Application.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace BookSharing.Auth.Application.Config
{
    public static class AuthConfiguration
    {
         public static IServiceCollection AddBookShareIdentity(this IServiceCollection services) {
            services
                .AddDbContext<AuthDbContext>(options => options.UseInMemoryDatabase("AuthDatabase"))
                .AddIdentity<AppUser, AppIdentityRole>(options =>
                    {
                        options.Password.RequireDigit = true;
                        options.Password.RequiredLength = 1;
                        options.Password.RequiredUniqueChars = 0;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireUppercase = false;
                    })
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();
            
            return services;
        }

        public static IServiceCollection AddBookShareAuthorization(this IServiceCollection services, AuthSettings settings)
        {
            services
                .AddAuthentication(options =>
                    {
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                .AddJwtBearer(options =>
                    {
                        options.SaveToken = true;
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidAudience = settings.Audience,
                            ValidIssuer = settings.Issuer,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Secret))
                        };
                    });

            return services;
        }
    }
}
