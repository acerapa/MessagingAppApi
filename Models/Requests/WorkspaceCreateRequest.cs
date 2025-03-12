using System.ComponentModel.DataAnnotations;

namespace MessagingApp.Models.Requests
{
    public class WorkspaceCreateRequest
    {
        [StringLength(255, MinimumLength = 3)]
        public required string Name { get; set; }
        public string? Description { get; set; } = null;
        public string? ImageUrl { get; set; } = null;
        public int? OwnerId { get; set; }
    }
}