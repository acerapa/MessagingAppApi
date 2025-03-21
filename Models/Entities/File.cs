namespace MessagingApp.Models.Entities
{
    public class File
    {
        public int Id { get; set; }
        public required string FileName { get; set; }
        public required byte[] Data { get; set; }
        public required string Extention { get; set; }
        public required string Type { get; set; }
    }
}
