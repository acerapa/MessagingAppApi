using System.Threading.Tasks;
using MessagingApp.Models.Requests;
using MessagingApp.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace MessagingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            bool isAuthenticated = await authService.AuthenticateUser(loginRequest);

            return Ok(isAuthenticated);
        }
    }
}