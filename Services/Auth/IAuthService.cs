using System.Security.Claims;
using MessagingApp.Models.Requests;

namespace MessagingApp.Services.Auth
{
    public interface IAuthService
    {
        public Task<bool> AuthenticateUser(LoginRequest request);

    }
}