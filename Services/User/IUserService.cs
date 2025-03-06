using MessagingApp.Models.DTOs;
using MessagingApp.Models.Entities;

namespace MessagingApp.Services.Users
{
    public interface IUserService {
        Task<User> CreateUserAysnc(UserCreateDTO createUserDTO);
    }
}