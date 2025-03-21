using MessagingApp.Models.DTOs.Requests;
using MessagingApp.Models.Entities;
using MessagingApp.Models.Responses;
using MessagingApp.Services.Workspaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace MessagingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkSpaceController(
        ILogger<WorkSpaceController> logger,
        IWorkspaceService workspaceService
    ) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<WorkspaceResponse[]>> Workspaces()
        {
            WorkspaceResponse[] workspaces = await workspaceService.GetWorkspacesAsync();
            return Ok(workspaces);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Workspace?>> Workspace(int id)
        {
            // Workspace? workspace = await workspaceService.GetWorkspaceAsync(id);
            Workspace? workspace = await workspaceService.TestWorkspace(id);
            if (workspace == null) return NotFound();
            return Ok(workspace);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Workspace?>> CreateWorkspace(WorkspaceCreateRequest workspaceCreateRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // get current user login
                if (workspaceCreateRequest.OwnerId == null)
                {
                    int userId = int.Parse(User.Claims.SingleOrDefault(c => c.Type == JwtRegisteredClaimNames.NameId)!.Value ?? "0");
                    logger.LogInformation("User Id: {userId}", userId);
                    workspaceCreateRequest.OwnerId = userId;
                }

                Workspace workspace = await workspaceService.CreateWorkspaceAsync(workspaceCreateRequest);
                return Ok(workspaceCreateRequest);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult> UpdateWorkspace(int id, WorkspaceUpdateRequest workspaceUpdateRequest)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                await workspaceService.UpdateWorkspaceAsync(id, workspaceUpdateRequest);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteWorkspace(int id)
        {
            try {
                await workspaceService.DeleteWorkspaceAsync(id);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex);
            }
        }
    }
}