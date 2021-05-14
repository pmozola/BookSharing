using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookSharing.Auth.Application.Infrastructure;
using BookSharing.Auth.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BookSharing.Auth.Application.CommandHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result>
    {
        private readonly UserManager<AppUser> _userManager;

        public RegisterUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByNameAsync(request.UserName) is not null)
            {
                return Result.Error(new ArgumentException("Username is taken"));
              
            }
            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email
            };
            var userCreated = await _userManager.CreateAsync(user, request.Password);
            
            if (!userCreated.Succeeded)
            {
                return Result.Error(new ArgumentException( (string.Join(", ", userCreated.Errors.Select(error => $"{error.Code} {error.Description}")))));
            }

            return Result.Success();
        }
    }
    public record RegisterUserCommand(string UserName, string Email, string Password) : IRequest<Result>;
}
