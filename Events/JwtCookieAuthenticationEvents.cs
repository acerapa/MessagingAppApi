using System.Text;
using MessagingApp.Configurations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace MessagingApp.Events
{
    public class JwtCookieAuthenticationEvents(IOptions<JwtSettings> jwtSettings) : CookieAuthenticationEvents
    {
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            string? cookieValue = context.Request.Cookies["messaging-app-auth"];

            if (string.IsNullOrEmpty(cookieValue))
            {
                context.RejectPrincipal();
                return;
            }

            try
            {
                var tokenHandler = new JsonWebTokenHandler();
                byte[] Key = Encoding.UTF8.GetBytes(jwtSettings.Value.Key);

                await tokenHandler.ValidateTokenAsync(cookieValue, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Value.ValidIssuer,
                    ValidAudience = jwtSettings.Value.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Key)
                });
            }

            catch (Exception)
            {
                context.RejectPrincipal();
            }
        }
    }
}