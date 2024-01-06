using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Coboss.Application.Services
{
    public class RolesService : IRolesService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RolesService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Role> GetByNameAsync(string name)
        {
            return await _applicationDbContext.Roles
                .FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task CreateAsync(Role role)
        {
            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    _applicationDbContext.Roles.Add(role);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception("Role create error");
                }
            }
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _applicationDbContext.Roles
                .Where(x => x.Name == name)
                .AnyAsync();
        }
    }
}
