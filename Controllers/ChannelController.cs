using MessagingApp.Models.DTOs.Requests;
using MessagingApp.Models.DTOs.Responses;
using MessagingApp.Services.Channels;
using Microsoft.AspNetCore.Mvc;

namespace MessagingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelController(
        IChannelService channelService
    ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllRelatedChannel(int workspaceId)
        {
            List<ChannelResponse> channels = await channelService.GetWorkspaceChannel(workspaceId);
            return Ok(channels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChannel(ChannelCreateRequest request)
        {
            try
            {
                ChannelResponse channel = await channelService.CreateChannel(request);
                return Ok(channel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateChannel(int Id, ChannelUpdateRequest request)
        {
            try
            {
                ChannelResponse channel = await channelService.UpdateChannel(Id, request);
                return Ok(channel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteChannel(int Id)
        {
            try
            {
                await channelService.DeleteChannel(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
