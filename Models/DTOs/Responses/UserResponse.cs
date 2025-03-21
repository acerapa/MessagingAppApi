using MessagingApp.Models.Responses;

namespace MessagingApp.Models.DTOs.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<WorkspaceShortResponse> Workspaces { get; set; } = [];
    }
}