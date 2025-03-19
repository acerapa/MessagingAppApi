using AutoMapper;
using MessagingApp.Models.DTOs;
using MessagingApp.Models.Entities;
using MessagingApp.Models.Responses;

namespace MessagingApp.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserUpdateDTO, User>()
                .ForAllMembers(opt => opt.Condition((source, destination, srcMember, destMember) => srcMember is not null));
            
            CreateMap<User, UserResponse>();
        }
    }
}
