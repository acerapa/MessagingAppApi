using AutoMapper;
using MessagingApp.Context;
using MessagingApp.Models.DTOs;
using MessagingApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessagingApp.Services.Users
{
    public class UserServices(ApplicationDbContext _context, IMapper mapper) : IUserService
    {
        public async Task<User> CreateUserAysnc(UserCreateDTO createUserDTO)
        {
            User user = new()
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

        public async Task<User?> GetUser(int id)
        {
            User? user = await _context.Users.FindAsync(id);

            return user;
        }

        public async Task<User[]> GetUsers()
        {
            User[] users = await _context.Users.ToArrayAsync<User>();

            return users;
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> UpdateUser(int id, UserUpdateDTO userUpdateDTO)
        {
            User? user = await _context.Users.FindAsync(id);
            if (user == null) return null;
            mapper.Map(userUpdateDTO, user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
