using System.Text;
using MessagingApp.Configurations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace MessagingApp.Events
{
    public class JwtCookieAuthenticationEvents(
        IOptions<JwtSettings> jwtSettings,
        ILogger<JwtCookieAuthenticationEvents> logger,
        IOptions<CookieSettings> cookieSettings
    ) : CookieAuthenticationEvents
    {
        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            string? cookieValue = context.Request.Cookies[cookieSettings.Value.CookieName];
            logger.LogInformation("Currently getting token: {cookieValue}", cookieValue);

            if (string.IsNullOrEmpty(cookieValue))
            {
                logger.LogInformation($"Here: Cookie is empty");
                context.RejectPrincipal();
                return;
            }

            try
            {
                var tokenHandler = new JsonWebTokenHandler();
                byte[] Key = Encoding.UTF8.GetBytes(jwtSettings.Value.AccessKey);

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
                logger.LogInformation($"Here: Cookie is not valid");
                context.RejectPrincipal();
            }
        }
    }
}