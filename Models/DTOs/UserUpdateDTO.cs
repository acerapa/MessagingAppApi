using System.ComponentModel.DataAnnotations;

namespace MessagingApp.Models.DTOs
{
    public class UserUpdateDTO
    {
        [MinLength(2)]
        public string? FirstName { get; set; }

        [MinLength(2)]
        public string? LastName { get; set; }

        public string? Email { get; set; }

    }
}