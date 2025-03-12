using System.Security.Claims;
using MessagingApp.Models.Entities;
using MessagingApp.Models.Requests;
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
        public ActionResult<List<object>> Workspaces()
        {
            logger.LogInformation("Workspaces requested");
            logger.LogInformation("Auth User {name}", User.FindFirstValue(JwtRegisteredClaimNames.Email));
            List<object> workspaces = [];
            workspaces.Add(new
            {
                Name = "Workspace 1",
                Id = 1,
                Description = "This is the first workspace"
            });

            // add more workspaces here with increasing id's
            workspaces.Add(new
            {
                Name = "Workspace 2",
                Id = 2,
                Description = "This is the second workspace"
            });
            workspaces.Add(new
            {
                Name = "Workspace 3",
                Id = 3,
                Description = "This is the third workspace"
            });
            workspaces.Add(new
            {
                Name = "Workspace 4",
                Id = 4,
                Description = "This is the fourth workspace"
            });

            return Ok(workspaces);
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
    }
}