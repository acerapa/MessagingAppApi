using AutoMapper;
using MessagingApp.Models.Entities;
using MessagingApp.Models.Requests;

namespace MessagingApp.MappingProfiles
{
    public class WorkspaceProfile : Profile
    {
        public WorkspaceProfile()
        {
            CreateMap<WorkspaceUpdateRequest, Workspace>()
                .ForMember(dest => dest.OwnerId, opt => opt.Ignore())
                .ForMember(dest => dest.Owner, opt => opt.Ignore())
                .ForAllMembers(opt => opt.MapFrom((src, dest, srcMember, destMember) => srcMember ?? destMember));
        }
    }
}