namespace MessagingApp.Configurations
{
    public class CookieSettings
    {
        public string CookieName { get; set; } = "messaging-app-auth";
        public string CookieNameRefresh { get; set; } = "messaging-app-auth-refresh";
        public int AccessMaxAge { get; set; } = 15;
        public int RefreshMaxAge { get; set; } = 1440;
    }
}