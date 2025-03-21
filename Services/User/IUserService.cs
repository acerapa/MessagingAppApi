using MessagingApp.Models.DTOs;
using MessagingApp.Models.DTOs.Responses;
using MessagingApp.Models.Entities;

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