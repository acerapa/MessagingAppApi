using System.Text;
using MessagingApp.Configurations;
using MessagingApp.Models.Responses;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace MessagingApp.Services.Token
{
    public class TokenService(
        IOptions<JwtSettings> jwtSettings,
        IOptions<CookieSettings> cookieSettings,
        IHttpContextAccessor httpContextAccessor
    ) : ITokenService
    {

        private string GenerateJwtToken(IDictionary<string, object> claims, bool isRefresh = false)
        {
            var tokenHandler = new JsonWebTokenHandler();
            byte[] Key = Encoding.UTF8.GetBytes(jwtSettings.Value.AccessKey);
            var Expires = jwtSettings.Value.ExpireAccessInMin;

            if (isRefresh)
            {
                claims.Add("isRefresh", true);
                Expires = jwtSettings.Value.ExpireRefreshInMin;
                Key = Encoding.UTF8.GetBytes(jwtSettings.Value.RefreshKey);
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Issuer = jwtSettings.Value.ValidIssuer,
                Audience = jwtSettings.Value.ValidAudience,
                Expires = DateTime.UtcNow.AddMinutes(Expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256)
            };

            return tokenHandler.CreateToken(tokenDescriptor);
        }

        public TokenResponse GenerateTokens(IDictionary<string, object> claims)
        {
            string accessToken = GenerateJwtToken(claims);
            string refreshToken = GenerateJwtToken(claims, true);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }

        public async Task<bool> ValidateTokenAsync(string? token, bool isRefresh = false)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }

            try
            {
                var tokenHandler = new JsonWebTokenHandler();
                string Key = jwtSettings.Value.AccessKey;

                if (isRefresh)
                {
                    Key = jwtSettings.Value.RefreshKey;
                }

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Value.ValidIssuer,
                    ValidAudience = jwtSettings.Value.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key)),
                };

                await tokenHandler.ValidateTokenAsync(token, tokenValidationParameters);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public TokenResponse RegenerateTokens(string refreshToken)
        {
            var tokenHandler = new JsonWebTokenHandler();
            var jsonWebToken = tokenHandler.ReadJsonWebToken(refreshToken);

            Dictionary<string, object> claims = [];
            foreach (var claim in jsonWebToken.Claims)
            {
                if (claim.Type != "isRefresh")
                {
                    claims[claim.Type] = claim.Value;
                }
            }

            return GenerateTokens(claims);
        }

        public void SetTokenToCookie(string token, bool isRefresh = false)
        {
            var httpContext = httpContextAccessor.HttpContext;

            int Expires = cookieSettings.Value.AccessMaxAge;
            string CookieName = cookieSettings.Value.CookieName;
            if (isRefresh)
            {
                Expires = cookieSettings.Value.RefreshMaxAge;
                CookieName = cookieSettings.Value.CookieNameRefresh;
            }

            var cookiesOptions = new CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddMinutes(Expires),
            };

            httpContext!.Response.Cookies.Append(CookieName, token, cookiesOptions);
        }
    }
}
