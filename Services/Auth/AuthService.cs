using System.Text;
using MessagingApp.Context;
using MessagingApp.Configurations;
using MessagingApp.Models.Entities;
using Microsoft.Extensions.Options;
using MessagingApp.Models.Requests;
using Microsoft.EntityFrameworkCore;
using MessagingApp.Services.Users.Passwords;
using MessagingApp.Models.Responses;
using MessagingApp.Services.Token;

namespace MessagingApp.Services.Auth
{
    public class AuthService(
        ITokenService tokenService,
        ApplicationDbContext context,
        IPasswordService passwordService,
        IOptions<CookieSettings> cookieSettings,
        IHttpContextAccessor httpContextAccessor
    ) : IAuthService
    {
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

                    TokenResponse tokens = tokenService.GenerateTokens(claims);
                    var httpContext = httpContextAccessor.HttpContext;
                    httpContext!.Response.Cookies.Append(cookieSettings.Value.CookieName, tokens.AccessToken);
                    httpContext!.Response.Cookies.Append(cookieSettings.Value.CookieNameRefresh, tokens.RefreshToken);
                }
            }

            return isAuthenticated;
        }
    }
}
