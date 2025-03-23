using MessagingApp.Models.DTOs.Responses;
using MessagingApp.Services.Channel;
using Microsoft.AspNetCore.Mvc;

namespace MessagingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController (
        IChannelService channelService
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRelatedChannel(int workspaceId)
        {
            List<ChannelResponse> channels = await channelService.GetWorkspaceChannel(workspaceId);
            return Ok(channels);
        }
    }
}
