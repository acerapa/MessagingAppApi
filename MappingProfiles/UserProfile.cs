using AutoMapper;
using MessagingApp.Models.DTOs;
using MessagingApp.Models.DTOs.Responses;
using MessagingApp.Models.Entities;

namespace MessagingApp.MappingProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserUpdateDTO, User>()
                .ForAllMembers(opt => opt.Condition((source, destination, srcMember, destMember) => srcMember is not null));

            CreateMap<User, UserResponse>();
            CreateMap<User, UserShortResponse>()
                .ForMember(destination => destination.FullName, opt => opt.MapFrom(source => $"{source.FirstName} {source.LastName}"));
        }
    }
}
