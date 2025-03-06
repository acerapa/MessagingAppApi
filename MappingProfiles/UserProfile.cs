using AutoMapper;
using MessagingApp.Models.DTOs;
using MessagingApp.Models.Entities;

namespace MessagingApp.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserUpdateDTO, User>();
        }
    }
}