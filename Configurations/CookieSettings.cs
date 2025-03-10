namespace MessagingApp.Configurations
{
    public class CookieSettings
    {
        public string CookieName { get; set; } = "messaging-app-auth";
        public int MaxAge { get; set; } = 30;
    }
}