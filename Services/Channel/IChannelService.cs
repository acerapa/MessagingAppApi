using MessagingApp.Models.DTOs.Responses;

namespace MessagingApp.Services.Channel
{
    public interface IChannelService
    {
        Task<List<ChannelResponse>> GetWorkspaceChannel (int workspaceId);
    }
}