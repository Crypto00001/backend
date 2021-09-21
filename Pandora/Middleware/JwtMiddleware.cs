using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Pandora.Application.Contract;
using Pandora.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Pandora.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
            if (userId != null)
            {
                // attach user to context on successful jwt validation
                UserSession.UserId = userId.Value;
                context.Items["User"] = userService.GetById(userId.Value);
            }

            await _next(context);
        }
    }
}
