namespace MessagingApp.Models.DTOs.Responses
{
    public class ChannelShortResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CreatedById { get; set; }
        public int WorkspaceId { get; set; }
    }
}
