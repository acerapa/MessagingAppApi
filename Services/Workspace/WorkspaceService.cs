using AutoMapper;
using AutoMapper.QueryableExtensions;
using MessagingApp.Context;
using MessagingApp.Models.Entities;
using MessagingApp.Models.Requests;
using MessagingApp.Models.Responses;
using MessagingApp.Services.Users;
using Microsoft.EntityFrameworkCore;

namespace MessagingApp.Services.Workspaces
{
    public class WorkspaceService(
        IUserService userService,
        ApplicationDbContext _context,
        IMapper mapper
    ) : IWorkspaceService
    {
        public async Task<Workspace?> GetWorkspaceAsync(int id)
        {
            return await _context.Workspaces.Include(w => w.Owner).SingleOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Workspace?> TestWorkspace(int id)
        {
            return await _context.Workspaces.SingleOrDefaultAsync(w => w.Id == id);
        }
        public async Task<WorkspaceResponse[]> GetWorkspacesAsync()
        {
            WorkspaceResponse[] workspaces = await _context.Workspaces
                .ProjectTo<WorkspaceResponse>(mapper.ConfigurationProvider)
                .ToArrayAsync();
            return workspaces;
        }

        public async Task<Workspace> CreateWorkspaceAsync(WorkspaceCreateRequest workspaceCreateRequest)
        {
            UserResponse? user = await userService.GetUser(workspaceCreateRequest.OwnerId ?? 0) ?? throw new Exception("User not found");
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

        public async Task UpdateWorkspaceAsync(int id, WorkspaceUpdateRequest updateRequest)
        {
            Workspace? workspace = await _context.Workspaces.SingleOrDefaultAsync(w => w.Id == id);

            if (workspace == null) throw new Exception("Workspace not found");

            mapper.Map(updateRequest, workspace);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteWorkspaceAsync(int id)
        {
            Workspace? workspace = await GetWorkspaceAsync(id);
            if (workspace == null) throw new Exception("Workspace not found");

            _context.Workspaces.Remove(workspace);
            await _context.SaveChangesAsync();
        }
    }
}