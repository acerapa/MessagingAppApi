using MessagingApp.Models.Entities;
using MessagingApp.Models.Requests;

namespace MessagingApp.Services.Workspaces
{
    public interface IWorkspaceService
    {
        Task<Workspace> GetWorkspaceAsync(int id);
        Task<Workspace> CreateWorkspaceAsync(WorkspaceCreateRequest workspace);
    }
}