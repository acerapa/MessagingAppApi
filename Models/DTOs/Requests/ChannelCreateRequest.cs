using System.ComponentModel.DataAnnotations;

namespace MessagingApp.Models.DTOs.Requests
{
    public class ChannelCreateRequest
    {
        [Required]
        public int WorkspaceId { get; set; }
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsPrivate { get; set; } = false;
    }
}
