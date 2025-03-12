using System.ComponentModel.DataAnnotations;

namespace MessagingApp.Models.Requests
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [StringLength(255, MinimumLength = 8)]
        public string Password { get; set; } = string.Empty;
    }
}
