using MessagingApp.Context;
using MessagingApp.Models.Entities;
using MessagingApp.Models.Requests;
using MessagingApp.Services.Users;

namespace MessagingApp.Services.Workspaces
{
    public class WorkspaceService(
        IUserService userService,
        ApplicationDbContext _context
    ) : IWorkspaceService
    {
        public Task<Workspace> GetWorkspaceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Workspace> CreateWorkspaceAsync(WorkspaceCreateRequest workspaceCreateRequest)
        {
            User? user = await userService.GetUser(workspaceCreateRequest.OwnerId ?? 0);
            if (user == null) throw new Exception("User not found");
            
            Workspace workspace = new()
            {
                Name = workspaceCreateRequest.Name,
                Description = workspaceCreateRequest.Description,
                ImageUrl = workspaceCreateRequest.ImageUrl,
                OwnerId = user.Id,
            };

            _context.Workspaces.Add(workspace);
            await _context.SaveChangesAsync();

            return workspace;
        }

    }
}