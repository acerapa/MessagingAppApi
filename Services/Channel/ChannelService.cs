using AutoMapper;
using AutoMapper.QueryableExtensions;
using MessagingApp.Context;
using MessagingApp.Models.DTOs.Responses;
using MessagingApp.Services.Channel;
using Microsoft.EntityFrameworkCore;

namespace MessagingApp.Services
{
    public class ChannelService(ApplicationDbContext context, IMapper mapper) : IChannelService
    {
        public async Task<List<ChannelResponse>> GetWorkspaceChannel(int workspaceId)
        {
            return await context.Channels.AsNoTracking()
                    .ProjectTo<ChannelResponse>(mapper.ConfigurationProvider)
                    .ToListAsync();
        }
    }
}
