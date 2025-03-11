using MessagingApp.Configurations;
using MessagingApp.Models.Requests;
using MessagingApp.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MessagingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService, IOptions<CookieSettings> cookieSettings) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            bool isAuthenticated = await authService.AuthenticateUser(loginRequest);

            return Ok(isAuthenticated);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            Response.Cookies.Delete(cookieSettings.Value.CookieName);
            Response.Cookies.Delete(cookieSettings.Value.CookieNameRefresh);
            await Task.CompletedTask;
            return Ok();
        }
    }
}
