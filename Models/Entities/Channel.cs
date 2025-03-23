using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MessagingApp.Models.Responses;

namespace MessagingApp.Models.Entities
{
    public class Channel
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool IsPrivate { get; set; } = false;
        
        [ForeignKey("Workspace")]
        public int WorkspaceId { get; set; }
        public Workspace Workspace { get; set; } = default!;
    }
}
