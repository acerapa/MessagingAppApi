using System.Text;
using MessagingApp.Context;
using MessagingApp.Configurations;
using MessagingApp.Models.Entities;
using Microsoft.Extensions.Options;
using MessagingApp.Models.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MessagingApp.Services.Users.Passwords;
using Microsoft.IdentityModel.JsonWebTokens;

namespace MessagingApp.Services.Auth
{
    public class AuthService(
        ApplicationDbContext context,
        IPasswordService passwordService,
        IOptions<JwtSettings> jwtSettings,
        IOptions<CookieSettings> cookieSettings,
        IHttpContextAccessor httpContextAccessor
    ) : IAuthService
    {
        public string GenerateJwtToken(IDictionary<string, object> claims)
        {
            var tokenHandler = new JsonWebTokenHandler();
            var key = Encoding.UTF8.GetBytes(jwtSettings.Value.Key);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Issuer = jwtSettings.Value.ValidIssuer,
                Audience = jwtSettings.Value.ValidAudience,
                Expires = DateTime.UtcNow.AddMinutes(jwtSettings.Value.ExpirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            return tokenHandler.CreateToken(tokenDescriptor);
        }
        public async Task<bool> AuthenticateUser(LoginRequest loginRequest)
        {
            bool isAuthenticated = false;

            User? user = await context.Users.SingleAsync(u => u.Email == loginRequest.Email);

            if (user != null)
            {
                isAuthenticated = await passwordService.VerifyPasswordAsync(loginRequest.Password, user.Password);
                if (isAuthenticated)
                {
                    IDictionary<string, object> claims = new Dictionary<string, object> {
                        { "UserId", user.Id },
                        { "Email", user.Email },
                    };

                    string token = GenerateJwtToken(claims);
                    var httpContext = httpContextAccessor.HttpContext;
                    httpContext!.Response.Cookies.Append(cookieSettings.Value.CookieName, token);
                }
            }

            return isAuthenticated;
        }
    }
}
