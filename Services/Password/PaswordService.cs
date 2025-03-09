using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;
using MessagingApp.Configurations;
using Microsoft.Extensions.Options;

namespace MessagingApp.Services.Users.Passwords
{
    public class PasswordService(IOptions<Argon2Settings> argonSettings) : IPasswordService
    {
        public async Task<string> HashPasswordAsync(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // using simple using statement to dispose of argon2 object
            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));

            argon2.Salt = salt;
            argon2.Iterations = argonSettings.Value.Iterations;
            argon2.MemorySize = argonSettings.Value.Memory;
            argon2.DegreeOfParallelism = argonSettings.Value.DegreeOfParallelism;

            byte[] hash = await argon2.GetBytesAsync(32);

            byte[] combineHash = new byte[salt.Length + hash.Length];
            Array.Copy(salt, 0, combineHash, 0, salt.Length);
            Array.Copy(hash, 0, combineHash, salt.Length, hash.Length);

            return Convert.ToBase64String(combineHash);
        }

        public async Task<bool> VerifyPasswordAsync(string password, string hashedPassword)
        {
            byte[] combineHash = Convert.FromBase64String(hashedPassword);

            byte[] salt = new byte[16];
            Array.Copy(combineHash, 0, salt, 0, 16);

            byte[] hash = new byte[combineHash.Length - salt.Length];
            Array.Copy(combineHash, salt.Length, hash, 0, hash.Length);

            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password));
            argon2.Salt = salt;
            argon2.Iterations = argonSettings.Value.Iterations;
            argon2.MemorySize = argonSettings.Value.Memory;
            argon2.DegreeOfParallelism = argonSettings.Value.DegreeOfParallelism;

            byte[] computedHash = await argon2.GetBytesAsync(hash.Length);

            return CryptographicOperations.FixedTimeEquals(computedHash, hash);
        }
    }
}