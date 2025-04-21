using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;

namespace MessagingApp.Controllers
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Retrieves the authenticated user's ID from the JWT claims.
        /// </summary>
        /// <returns>The user ID as an integer, or null if no user ID is found.</returns>
        protected int? GetAuthUserIdFromCliams ()
        {
            var userId = User.Claims.SingleOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId);
            
            return userId != null ? int.Parse(userId.Value) : null;
        }
    }
}