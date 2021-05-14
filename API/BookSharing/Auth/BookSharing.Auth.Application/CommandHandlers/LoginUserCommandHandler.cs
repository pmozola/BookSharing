using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BookSharing.Auth.Application.Infrastructure;
using BookSharing.Auth.Application.Results;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace BookSharing.Auth.Application.CommandHandlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<TokenResponse>>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AuthSettings _authSettings;

        public LoginUserCommandHandler(UserManager<AppUser> userManager, IOptions<AuthSettings> authSettings)
        {
            _userManager = userManager;
            _authSettings = authSettings.Value;
        }

        public async Task<Result<TokenResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                return Result<TokenResponse>.Error(
                    new ArgumentException($"User with name {request.Email} couldn't be found"));
            }
               
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authSettings.Secret));

            var token = new JwtSecurityToken(
                issuer: _authSettings.Issuer,
                audience: _authSettings.Audience,
                expires: DateTime.Now.AddDays(1),
                claims: GetUserClaims(user),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            return Result<TokenResponse>.Success(new TokenResponse(new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo));

        }

        private static List<Claim> GetUserClaims(AppUser user)
        {
            return new()
            {
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Iat, user.Id.ToString()),
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };
        }
    }

    public record LoginUserCommand(string Email, string Password) : IRequest<Result<TokenResponse>>;
    public record TokenResponse(string Token, DateTime Expiration);
}
