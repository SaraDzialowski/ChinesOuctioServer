using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Extensions;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using ChinesOuctionServer.Models;

namespace webapi.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger;
        private static IConfiguration _config;


        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger, IConfiguration config)
        {
            _next = next;
            _logger = logger;
            _config = config;

        }

        public async Task InvokeAsync(HttpContext context)
        {
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var handler = new JwtSecurityTokenHandler();
            var b = context.Request.Headers["Authorization"].ToString();
            var tokenSecure = handler.ReadToken(context.Request.Headers["Authorization"]) as SecurityToken;
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(context.Request.Headers["Authorization"], validations, out tokenSecure);
            var prinicpal = (ClaimsPrincipal)Thread.CurrentPrincipal;

            User user = new User();

            int id;
            int.TryParse(claims.Claims.FirstOrDefault(x => x.Type == "Id")?.Value ?? "", out id);
            user.Id = id;
            EnumRole role;
            Enum.TryParse(claims.Claims.FirstOrDefault(x => x.Type == "Role")?.Value ?? "", out role);
            user.Role = role;
            //var emailClaim = claims.Claims.FirstOrDefault(x => x.Type == "Email");
            //if (emailClaim != null)
            //{
            //    user.Email = emailClaim.Value;
            //}
            context.Items["User"] = user;
            await _next(context);
        }

    }
}


