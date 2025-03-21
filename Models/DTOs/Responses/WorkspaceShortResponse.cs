namespace MessagingApp.Models.Responses
{
    public class WorkspaceShortResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set;}
        public string? ImageUrl { get; set;}
    }
}