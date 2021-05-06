using System.IdentityModel.Tokens.Jwt;

using BookSharing.Application.Interface;
using Microsoft.AspNetCore.Http;

namespace BookSharing.API.Infrastructure
{
    public class HttpContextUser : IUserContext
    {
        private readonly IHttpContextAccessor _contextAccesso;

        public HttpContextUser(IHttpContextAccessor ctxAccessor)
        {
            _contextAccesso = ctxAccessor;
        }

        public int GetUserId()
        {
            var userIdClaim = _contextAccesso.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Iat);

            return int.Parse(userIdClaim.Value);
        }
    }
}
