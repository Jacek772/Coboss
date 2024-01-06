using Coboss.Application.Extensions;
using Coboss.Application.Functions.Commands.Projects;
using Coboss.Application.Functions.Query.Projects;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Persistance;
using Coboss.Types.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Coboss.Application.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IGlobalSettingsService _globalSettingsService;
        private readonly IObjectCodesService _objectCodesService;

        public ProjectsService(ApplicationDbContext applicationDbContext,
            IGlobalSettingsService globalSettingsService, IObjectCodesService objectCodesService)
        {
            _applicationDbContext = applicationDbContext;
            _globalSettingsService = globalSettingsService;
            _objectCodesService = objectCodesService;
        }

        public async Task<Project> GetByIdAsync(int id)
        {
            return await _applicationDbContext.Projects
                .Include(x => x.Manager)
                .Include(x => x.BusinnessTasks)
                .ThenInclude(x => x.TaskRealisations)
                .Include(x => x.BusinnessTasks)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Project>> GetAsync(GetProjectsQuery query)
        {
            IQueryable<Project> projects = _applicationDbContext.Projects
                .Include(x => x.Manager)
                .Include(x => x.BusinnessTasks)
                .ThenInclude(x => x.TaskRealisations)
                .Include(x => x.BusinnessTasks);

            if(!string.IsNullOrEmpty(query?.SearchText))
            {
                projects = projects
                    .Where(x => EF.Functions.ILike(x.Name, $"%{query.SearchText}%")
                       || EF.Functions.ILike(x.Description, $"%{query.SearchText}%")
                       || EF.Functions.ILike(x.Number, $"%{query.SearchText}%")
                       || EF.Functions.ILike(x.Manager.Code, $"%{query.SearchText}%")
                       || EF.Functions.ILike(x.Manager.Name, $"%{query.SearchText}%")
                       || EF.Functions.ILike(x.Manager.Surname, $"%{query.SearchText}%")
                       || EF.Functions.ILike(x.Manager.PESEL, $"%{query.SearchText}%")
                       || EF.Functions.ILike(x.Manager.NIP, $"%{query.SearchText}%"));
            }

            if (!string.IsNullOrEmpty(query?.OrderBy) && !string.IsNullOrEmpty(query?.OrderBy))
            {
                projects = projects.ApplaySort(query.OrderBy);
            }

            return await projects.ToListAsync();
        }

        public async Task CreateAsync(Project project)
        {
            project.Number = await GetNewProjectNumber();

            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    await _applicationDbContext.Projects.AddAsync(project);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Project create error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            Project project = await _applicationDbContext.Projects.FirstOrDefaultAsync(x => x.Id == id);
            if (project == null)
            {
                throw new BadRequestException($"Project with id = {id} cannot exits");
            }

            using (IDbContextTransaction transaction = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _applicationDbContext.Projects.Remove(project);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Employee delete error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task DeleteAsync(int[] ids)
        {
            List<Project> projects = await _applicationDbContext.Projects
               .Where(x => ids.Contains(x.Id))
               .ToListAsync();

            if (projects.Count == 0)
            {
                throw new Exception($"Projects with passed ids not exits");
            }

            using (IDbContextTransaction transaction = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _applicationDbContext.Projects.RemoveRange(projects);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Projects delete error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task UpdateAsync(Project project)
        {
            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    _applicationDbContext.Projects.Update(project);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Project update error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task UpdateAsync(UpdateProjectCommand command)
        {
            Project project = await _applicationDbContext.Projects.FirstOrDefaultAsync(x => x.Id == command.Id);
            if(project == null)
            {
                throw new Exception($"Project with id = {command.Id} cannot exits");
            }

            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    if(command.Name is string name)
                    {
                        project.Name = name;
                    }

                    if(command.Description is string description)
                    {
                        project.Description = description;
                    }

                    if(command.Term is DateTime term)
                    {
                        project.Term = term;
                    }

                    if(command.ManagerId is int managerId)
                    {
                        project.Manager = await _applicationDbContext.Employees.FirstOrDefaultAsync(x => x.Id == managerId);
                    }

                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Project update error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task<bool> ExistsAsync()
        {
            return await _applicationDbContext.Projects.AnyAsync();
        }

        private async Task<string> GetNewProjectNumber()
        {
            int codeLength = await _globalSettingsService
                .GetValueIntAsync(GlobalSetting.GlobalSettingKey.ProjectNumberLength);

            ObjectCode objectCode = await _objectCodesService.GetNewObjectCode<Project>(codeLength);
            return objectCode.Code;
        }

    }
}
