using AutoMapper;
using MessagingApp.Models.DTOs.Requests;
using MessagingApp.Models.DTOs.Responses;
using MessagingApp.Models.Entities;

namespace MessagingApp.MappingProfiles
{
    public class ChannelProfile : Profile
    {
        public ChannelProfile()
        {
            CreateMap<ChannelCreateRequest, Channel>();
            CreateMap<Channel, ChannelResponse>();
            CreateMap<ChannelUpdateRequest, Channel>()
                .ForAllMembers(opt => opt.Condition((source, destination, srcMember, destMember) => srcMember is not null));
        }
    }
}
