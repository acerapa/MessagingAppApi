using System.ComponentModel.DataAnnotations;

namespace MessagingApp.Models.DTOs
{
    public class UserUpdateDTO
    {
        [StringLength(255, MinimumLength = 2)]
        public string? FirstName { get; set; }

        [StringLength(255, MinimumLength = 2)]
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

    }
}