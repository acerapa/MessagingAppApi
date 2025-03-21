using AutoMapper;
using MessagingApp.Models.DTOs.Requests;
using MessagingApp.Models.Entities;
using MessagingApp.Models.Responses;

namespace MessagingApp.MappingProfiles
{
    public class WorkspaceProfile : Profile
    {
        public WorkspaceProfile()
        {
            CreateMap<WorkspaceUpdateRequest, Workspace>()
                .ForAllMembers(opt => opt.Condition((source, destination, srcMember, destMember) => srcMember is not null));

            CreateMap<Workspace, WorkspaceResponse>();
            CreateMap<Workspace, WorkspaceShortResponse>();
        }
    }
}
