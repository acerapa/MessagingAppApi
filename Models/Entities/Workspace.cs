using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MessagingApp.Models.Entities
{
    public class Workspace
    {
        [Key]
        public int Id { get; set;}

        public required string Name { get; set;}
        public string? Description { get; set;}
        public string? ImageUrl { get; set;} = "";

        [ForeignKey("User")]
        public required int OwnerId { get; set;}

        public virtual User Owner { get; set;} = null!;
    }
}
