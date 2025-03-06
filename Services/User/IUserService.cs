using MessagingApp.Models.DTOs;
using MessagingApp.Models.Entities;

namespace MessagingApp.Services.Users
{
    public interface IUserService
    {
        Task<User?> GetUser(int id);
        Task<User[]> GetUsers();
        Task<User> CreateUserAysnc(UserCreateDTO createUserDTO);
        Task<User?> UpdateUser(int id, UserUpdateDTO updateUserDTO);
        Task DeleteUser(User user);
    }

}