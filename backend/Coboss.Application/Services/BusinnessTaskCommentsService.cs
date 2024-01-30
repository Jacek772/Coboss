using Coboss.Application.Extensions;
using Coboss.Application.Functions.Commands.BusinnessTaskComments;
using Coboss.Application.Functions.Query.BusinnessTaskComments;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Coboss.Application.Services
{
    public class BusinnessTaskCommentsService : IBusinnessTaskCommentsService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BusinnessTaskCommentsService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<BusinnessTaskComment>> GetAsync(GetBusinnessTaskCommentsQuery query)
        {
            IQueryable<BusinnessTaskComment> businnessTaskComments = _applicationDbContext.BusinnessTaskComments
                .Include(x => x.User);

            if (!string.IsNullOrEmpty(query.SearchText))
            {
                businnessTaskComments = businnessTaskComments
                    .Where(x => x.Text.ToLower().Contains(query.SearchText.ToLower())
                        || x.User.Email.ToLower().Contains(query.SearchText.ToLower()));
            }

            if (!string.IsNullOrEmpty(query?.OrderBy) && !string.IsNullOrEmpty(query?.OrderBy))
            {
                businnessTaskComments = businnessTaskComments.ApplaySort(query.OrderBy);
            }
            return await businnessTaskComments.ToListAsync();
        }

        public async Task CreateAsync(BusinnessTaskComment taskComment)
        {
            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    await _applicationDbContext.BusinnessTaskComments.AddAsync(taskComment);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"BusinnessTaskComment create error\n{ex.ToMessage()}");
                }
            }
        }

        public async Task UpdateAsync(BusinnessTaskComment businnessTaskComment)
        {
            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    _applicationDbContext.BusinnessTaskComments.Update(businnessTaskComment);
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

        public async Task UpdateAsync(UpdateBusinnessTaskCommentCommand command)
        {
            BusinnessTaskComment businnessTaskComment = await _applicationDbContext.BusinnessTaskComments.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (businnessTaskComment == null)
            {
                throw new Exception($"BusinnessTaskComment with id = {command.Id} not exits");
            }

            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    if (command.Date is DateTime date)
                    {
                        businnessTaskComment.Date = date;
                    }

                    if (command.Text is string text)
                    {
                        businnessTaskComment.Text = text;
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
            return await _applicationDbContext.BusinnessTaskComments.AnyAsync();
        }

        public async Task DeletesAsync(int[] ids)
        {
            List<BusinnessTaskComment> businnessTaskComments = await _applicationDbContext.BusinnessTaskComments
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            if (businnessTaskComments.Count == 0)
            {
                throw new Exception($"BusinnessTaskComments with passed ids not exits");
            }

            using (IDbContextTransaction transaction = await _applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _applicationDbContext.BusinnessTaskComments.RemoveRange(businnessTaskComments);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"BusinnessTaskComments delete error\n{ex.ToMessage()}");
                }
            }
        }
    }
}
