using MessagingApp.Models.Entities;
using MessagingApp.Models.Requests;
using MessagingApp.Models.Responses;

namespace MessagingApp.Services.Workspaces
{
    public interface IWorkspaceService
    {
        Task<Workspace?> GetWorkspaceAsync(int id);
        Task<WorkspaceResponse[]> GetWorkspacesAsync();
        Task<Workspace> CreateWorkspaceAsync(WorkspaceCreateRequest workspace);
        Task UpdateWorkspaceAsync(int id, WorkspaceUpdateRequest workspaceUpdate);
        Task DeleteWorkspaceAsync(int id);
        Task<Workspace?> TestWorkspace(int id);
    }
}
