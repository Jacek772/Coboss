using Coboss.Application.Extensions;
using Coboss.Application.Functions.Commands.BusinnessTasks;
using Coboss.Application.Functions.Query.BusinnessTasks;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Persistance;
using Coboss.Types.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Coboss.Application.Services
{
    public class BusinnessTasksService : IBusinnessTasksService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BusinnessTasksService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<BusinnessTask> GetBusinnessTasksAsync(int id)
        {
            return await _applicationDbContext.BusinnessTasks
                .Include(x => x.TaskRealisations)
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<BusinnessTask>> GetBusinnessTasksAsync(GetBusinnessTasksQuery query)
        {
            IQueryable<BusinnessTask> businnessTasks = _applicationDbContext.BusinnessTasks
                .Include(x => x.TaskRealisations)
                .Include(x => x.Comments);

            if(!string.IsNullOrEmpty(query.SearchText))
            {
                businnessTasks = businnessTasks
                    .Where(x => EF.Functions.ILike(x.Name, $"%{query.SearchText}%")
                       || EF.Functions.ILike(x.Description, $"%{query.SearchText}%"));
            }

            if (!string.IsNullOrEmpty(query?.OrderBy) && !string.IsNullOrEmpty(query?.OrderBy))
            {
                businnessTasks = businnessTasks.ApplaySort(query.OrderBy);
            }
            return await businnessTasks.ToListAsync();
        }

        public async Task CreateBusinnessTaskAsync(BusinnessTask task)
        {
            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    await _applicationDbContext.BusinnessTasks.AddAsync(task);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"BusinnessTask create error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task UpdateBusinnessTaskAsync(BusinnessTask businnessTask)
        {
            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    _applicationDbContext.BusinnessTasks.Update(businnessTask);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"BusinnessTask update error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task UpdateBusinnessTaskAsync(UpdateBusinnessTaskCommand command)
        {
            BusinnessTask businnessTask = await _applicationDbContext.BusinnessTasks.FirstOrDefaultAsync(x => x.Id == command.Id);
            if(businnessTask == null)
            {
                throw new Exception($"BusinnessTask with id = {command.Id} not exits");
            }

            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    if (command.Name is string name)
                    {
                        businnessTask.Name = name;
                    }

                    if (command.Term is DateTime term)
                    {
                        businnessTask.Term = term;
                    }

                    if (command.Description is string description)
                    {
                        businnessTask.Description = description;
                    }

                    if (command.Date is DateTime date)
                    {
                        businnessTask.Date = date;
                    }

                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"BusinnessTask update error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task<bool> BusinnessTasksExistsAsync()
        {
            return await _applicationDbContext.BusinnessTasks.AnyAsync();
        }

        public async Task DeleteBusinnessTaskAsync(int id)
        {
            BusinnessTask businnessTask = await _applicationDbContext.BusinnessTasks.FirstOrDefaultAsync(x => x.Id == id);
            if (businnessTask == null)
            {
                throw new BadRequestException($"BusinnessTask with id = {id} not exits");
            }

            using (IDbContextTransaction transaction = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _applicationDbContext.BusinnessTasks.Remove(businnessTask);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"BusinnessTask delete error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task DeleteBusinnessTasksAsync(int[] ids)
        {
            List<BusinnessTask> businnessTasks = await _applicationDbContext.BusinnessTasks
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            if (businnessTasks.Count == 0)
            {
                throw new Exception($"BusinnessTasks with passed ids not exits");
            }

            using (IDbContextTransaction transaction = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _applicationDbContext.BusinnessTasks.RemoveRange(businnessTasks);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"BusinnessTasks delete error\n{ex.ToMessage()}");
                }
            }
        }
    }
}
