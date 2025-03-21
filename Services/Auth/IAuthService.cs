using MessagingApp.Models.DTOs.Requests;

namespace MessagingApp.Services.Auth
{
    public interface IAuthService
    {
        public Task<bool> AuthenticateUser(LoginRequest request);

    }
}