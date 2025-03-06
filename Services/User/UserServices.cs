using MessagingApp.Context;
using MessagingApp.Models.DTOs;
using MessagingApp.Models.Entities;

namespace MessagingApp.Services.Users
{
    public class UserServices(ApplicationDbContext _context) : IUserService
    {
        public async Task<User> CreateUserAysnc(UserCreateDTO createUserDTO)
        {
            User user = new User
            {
                FirstName = createUserDTO.FirstName,
                LastName = createUserDTO.LastName,
                Email = createUserDTO.Email,
                Password = createUserDTO.Password
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
