namespace MessagingApp.Configurations
{
    public class Argon2Settings
    {
        public int Iterations { get; set; } = 3;
        
        public int Memory { get; set; } = 64 * 1024;
        public int DegreeOfParallelism { get; set; } = 1;
    }
}