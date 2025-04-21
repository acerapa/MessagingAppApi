using MessagingApp.Models.DTOs.Requests;
using MessagingApp.Models.DTOs.Responses;
using MessagingApp.Models.Entities;

namespace MessagingApp.Services.Channels
{
    public interface IChannelService
    {
        Task<List<ChannelResponse>> GetWorkspaceChannel (int workspaceId);
        Task<ChannelShortResponse> CreateChannel (int userId, ChannelCreateRequest request);
        Task<Channel?> GetChannelById (int id, bool isNotTracking = false);
        Task<ChannelResponse> UpdateChannel (int Id, ChannelUpdateRequest request);
        Task DeleteChannel (int Id);
    }
}
