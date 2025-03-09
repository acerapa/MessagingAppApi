using System.ComponentModel.DataAnnotations;

namespace MessagingApp.Models.DTOs
{
    public class UserCreateDTO
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 8)]
        public required string Password { get; set; }
    }
}
