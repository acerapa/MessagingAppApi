namespace MessagingApp.Configurations
{
    public class JwtSettings
    {
        public string AccessKey { get; set; } = string.Empty;
        public string RefreshKey { get; set; } = string.Empty;
        public string ValidAudience { get; set; } = string.Empty;
        public string ValidIssuer { get; set; } = string.Empty;
        public int ExpireRefreshInMin { get; set; } = 1440;
        public int ExpireAccessInMin { get; set; } = 15;
    }
}
