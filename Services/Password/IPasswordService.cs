namespace MessagingApp.Services.Users.Passwords
{
    public interface IPasswordService
    {
        Task<string> HashPasswordAsync(string password);
        Task<bool> VerifyPasswordAsync(string password, string hashedPassword);
    }
}
