using Coboss.Application.Functions.Commands.Projects;
using Coboss.Application.Functions.Query.Projects;
using Coboss.Core.Entities;

namespace Coboss.Application.Services.Abstracts
{
    public interface IProjectsService
    {
        Task<Project> GetByIdAsync(int id);
        Task<List<Project>> GetAsync(GetProjectsQuery query);
        Task CreateAsync(Project project);
        Task DeleteAsync(int id);
        Task DeleteAsync(int[] id);
        Task UpdateAsync(Project project);
        Task UpdateAsync(UpdateProjectCommand command);
        Task<bool> ExistsAsync();
    }
}
