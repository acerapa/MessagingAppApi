using AutoMapper;
using MessagingApp.Models.DTOs;
using MessagingApp.Models.Entities;

namespace MessagingApp.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserUpdateDTO, User>()
                .ForAllMembers(opt => opt.MapFrom((src, dest, srcMember, destMember) => srcMember ?? destMember));
        }
    }
}