using System.ComponentModel.DataAnnotations;

namespace MessagingApp.Models.DTOs.Requests
{
    public class WorkspaceUpdateRequest
    {
        [StringLength(255, MinimumLength = 2)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}