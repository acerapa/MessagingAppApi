using MessagingApp.Models.DTOs.Responses;

namespace MessagingApp.Models.Responses
{
    public class WorkspaceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public int OwnerId { get; set; }
        public UserShortResponse Owner { get; set; } = null!;
    }
}
