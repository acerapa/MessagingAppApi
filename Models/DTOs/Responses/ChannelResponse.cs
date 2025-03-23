using MessagingApp.Models.Responses;

namespace MessagingApp.Models.DTOs.Responses
{
    public class ChannelResponse
    {
        public int Id { get; set; }
        public int WorkspaceId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public WorkspaceResponse Workspace { get; set; } = default!;
    }
}
