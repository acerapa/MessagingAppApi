using System.Text;
using MessagingApp.Context;
using MessagingApp.Models.Entities;
using MessagingApp.Models.Requests;
using Microsoft.EntityFrameworkCore;
using MessagingApp.Services.Users.Passwords;
using MessagingApp.Models.Responses;
using MessagingApp.Services.Token;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace MessagingApp.Services.Auth
{
    public class AuthService(
        ITokenService tokenService,
        ApplicationDbContext context,
        IPasswordService passwordService,
        IHttpContextAccessor httpContextAccessor
    ) : IAuthService
    {
        public async Task<bool> AuthenticateUser(LoginRequest loginRequest)
        {
            bool isAuthenticated = false;

            User? user = await context.Users.SingleOrDefaultAsync(u => u.Email == loginRequest.Email);

            if (user != null)
            {
                isAuthenticated = await passwordService.VerifyPasswordAsync(loginRequest.Password, user.Password);
                if (isAuthenticated)
                {
                    List<Claim> claims = [
                        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),   
                    ];

                    TokenResponse tokens = tokenService.GenerateTokens(claims);
                    var httpContext = httpContextAccessor.HttpContext;
                    tokenService.SetTokenToCookie(tokens.AccessToken);
                    tokenService.SetTokenToCookie(tokens.RefreshToken, true);
                }
            }

            return isAuthenticated;
        }
    }
}
