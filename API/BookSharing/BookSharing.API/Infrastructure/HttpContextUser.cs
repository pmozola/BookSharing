using System.IdentityModel.Tokens.Jwt;

using BookSharing.Application.Interface;
using Microsoft.AspNetCore.Http;

namespace BookSharing.API.Infrastructure
{
    public class HttpContextUser : IUserContext
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextUser(IHttpContextAccessor ctxAccessor)
        {
            _contextAccessor = ctxAccessor;
        }

        public int GetUserId()
        {
            var userIdClaim = _contextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Iat);

            return int.Parse(userIdClaim.Value);
        }
    }
}
