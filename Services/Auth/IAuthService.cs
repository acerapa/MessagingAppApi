using System.Security.Claims;
using MessagingApp.Models.Requests;

namespace MessagingApp.Services.Auth
{
    public interface IAuthService
    {
        public string GenerateJwtToken(IDictionary<string, object> claims);

        public Task<bool> AuthenticateUser(LoginRequest request);

        public Task LogoutUser();
    }
}