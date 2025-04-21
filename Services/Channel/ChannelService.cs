using AutoMapper;
using AutoMapper.QueryableExtensions;
using MessagingApp.Context;
using MessagingApp.Models.DTOs.Requests;
using MessagingApp.Models.DTOs.Responses;
using MessagingApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace MessagingApp.Services.Channels
{
    public class ChannelService(
        ApplicationDbContext context,
        IMapper mapper
    ) : IChannelService
    {
        public async Task<ChannelShortResponse> CreateChannel(int userId, ChannelCreateRequest request)
        {
            Channel channelToAdd = mapper.Map<Channel>(request);
            
            channelToAdd.CreatedById = userId;
            
            context.Channels.Add(channelToAdd);

            await context.SaveChangesAsync();

            return mapper.Map<ChannelShortResponse>(channelToAdd);
        }

        public async Task DeleteChannel(int Id)
        {
            Channel channelToDelete = await GetChannelById(Id) ??
                throw new ArgumentException($"Channel with {Id} id not found!");

            context.Channels.Remove(channelToDelete);
            await context.SaveChangesAsync();
            
            return;
        }

        public Task<Channel?> GetChannelById(int id, bool isNotTracking = false)
        {
            IQueryable<Channel> query = context.Channels.AsQueryable();

            if (isNotTracking)
                query = query.AsNoTracking();

            return query.SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<ChannelResponse>> GetWorkspaceChannel(int workspaceId)
        {
            return await context.Channels.AsNoTracking()
                    .Where(c => c.WorkspaceId == workspaceId)
                    .ProjectTo<ChannelResponse>(mapper.ConfigurationProvider)
                    .ToListAsync();
        }

        public async Task<ChannelResponse> UpdateChannel(int Id, ChannelUpdateRequest request)
        {
            Channel? channelToUpdate = await GetChannelById(Id) ??
                throw new ArgumentException($"Channel with {Id} id not found!");

            mapper.Map(request, channelToUpdate);
            await context.SaveChangesAsync();

            return mapper.Map<ChannelResponse>(channelToUpdate);
        }
    }
}
