using System.Security.Claims;
using MessagingApp.Models.Responses;

namespace MessagingApp.Services.Token
{
    public interface ITokenService
    {
        TokenResponse GenerateTokens(List<Claim> claims);
        TokenResponse RegenerateTokens(string refreshToken);
        Task<bool> ValidateTokenAsync(string token, bool isRefresh = false);
        void SetTokenToCookie (string token, bool isRefresh = false);
    }
}