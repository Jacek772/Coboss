using Coboss.Application.Extensions;
using Coboss.Application.Functions.Commands.BusinnessTaskRealisations;
using Coboss.Application.Functions.Query.BusinnessTaskRealisations;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Coboss.Application.Services
{
    public class BusinnessTaskRealisationsService : IBusinnessTaskRealisationsService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BusinnessTaskRealisationsService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<BusinnessTaskRealisation>> GetAsync(GetBusinnessTaskRealisationsQuery query)
        {
            IQueryable<BusinnessTaskRealisation> businnessTaskRealisations = _applicationDbContext.BusinnessTaskRealisations;

            if (!string.IsNullOrEmpty(query.SearchText))
            {
                businnessTaskRealisations = businnessTaskRealisations
                    .Where(x => EF.Functions.ILike(x.Description, $"%{query.SearchText}%"));
            }

            if (!string.IsNullOrEmpty(query?.OrderBy) && !string.IsNullOrEmpty(query?.OrderBy))
            {
                businnessTaskRealisations = businnessTaskRealisations.ApplaySort(query.OrderBy);
            }
            return await businnessTaskRealisations.ToListAsync();
        }

        public async Task CreateAsync(BusinnessTaskRealisation taskRealisation)
        {
            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    await _applicationDbContext.BusinnessTaskRealisations.AddAsync(taskRealisation);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"BusinnessTaskRealisation create error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task UpdateAsync(UpdateBusinnessTaskRealisationCommand command)
        {
            BusinnessTaskRealisation businnessTaskRealisation = await _applicationDbContext.BusinnessTaskRealisations.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (businnessTaskRealisation == null)
            {
                throw new Exception($"BusinnessTaskRealisation with id = {command.Id} not exits");
            }

            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    if (command.Date is DateTime date)
                    {
                        businnessTaskRealisation.Date = date;
                    }

                    if (command.TimeSpan is TimeSpan timeSpan)
                    {
                        businnessTaskRealisation.TimeSpan = timeSpan;
                    }

                    if (command.Description is string description)
                    {
                        businnessTaskRealisation.Description = description;
                    }

                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"BusinnessTaskRealisation update error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task<bool> ExistsAsync()
        {
            return await _applicationDbContext.BusinnessTaskRealisations.AnyAsync();
        }

        public async Task DeleteAsync(int[] ids)
        {
            List<BusinnessTaskRealisation> businnessTaskRealisations = await _applicationDbContext.BusinnessTaskRealisations
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            if (businnessTaskRealisations.Count == 0)
            {
                throw new Exception($"BusinnessTaskRealisations with passed ids not exits");
            }

            using (IDbContextTransaction transaction = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _applicationDbContext.BusinnessTaskRealisations.RemoveRange(businnessTaskRealisations);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"BusinnessTaskRealisations delete error\n{ex.ToMessage()}");
                }
            }
        }
    }
}

