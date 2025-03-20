using MessagingApp.Models.DTOs;
using MessagingApp.Models.Entities;
using MessagingApp.Models.Responses;

namespace MessagingApp.Services.Users
{
    public interface IUserService
    {
        Task<UserResponse?> GetUser(int id);
        Task<UserResponse[]> GetUsers();
        Task<User> CreateUserAysnc(UserCreateDTO createUserDTO);
        Task<User?> UpdateUser(int id, UserUpdateDTO updateUserDTO);
        Task DeleteUser(UserResponse user);
    }

}