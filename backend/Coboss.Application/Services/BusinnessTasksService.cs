using Coboss.Application.Extensions;
using Coboss.Application.Functions.Commands.BusinnessTaskComments;
using Coboss.Application.Functions.Commands.BusinnessTaskRealisations;
using Coboss.Application.Functions.Commands.BusinnessTasks;
using Coboss.Application.Functions.Query.BusinnessTasks;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Persistance;
using Coboss.Types.DTO;
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
                .ThenInclude(x => x.Employee)
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .Include(x => x.Project)
                .Include(x => x.BusinnessTasksEmployees)
                .ThenInclude(x => x.Employee)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<BusinnessTask>> GetBusinnessTasksAsync(GetBusinnessTasksQuery query)
        {
            IQueryable<BusinnessTask> businnessTasks = _applicationDbContext.BusinnessTasks
                .Include(x => x.TaskRealisations)
                .ThenInclude(x => x.Employee)
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .Include(x => x.Project)
                .Include(x => x.BusinnessTasksEmployees)
                .ThenInclude(x => x.Employee);

            if(!string.IsNullOrEmpty(query.SearchText))
            {
                businnessTasks = businnessTasks.Where(x =>
                    x.Name.ToLower().Contains(query.SearchText.ToLower())
                    || x.Description.ToLower().Contains(query.SearchText.ToLower()));
            }

            if (!string.IsNullOrEmpty(query?.OrderBy) && !string.IsNullOrEmpty(query?.OrderBy))
            {
                businnessTasks = businnessTasks.ApplaySort(query.OrderBy);
            }

            if(query?.DateFrom is DateTime dateFrom)
            {
                businnessTasks = businnessTasks.Where(x => x.Date >= dateFrom);
            }

            if (query?.DateTo is DateTime dateTo)
            {
                businnessTasks = businnessTasks.Where(x => x.Date <= dateTo);
            }

            if(query?.TermFrom is DateTime termFrom)
            {
                businnessTasks = businnessTasks.Where(x => x.Term >= termFrom);
            }

            if (query?.TermTo is DateTime termTo)
            {
                businnessTasks = businnessTasks.Where(x => x.Term <= termTo);
            }

            if(query?.ProjectId is int projectId)
            {
                businnessTasks = businnessTasks.Where(x => x.ProjectId == projectId);
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

                    if(command.ProjectId is int projectId)
                    {
                        businnessTask.ProjectId = projectId;
                    }

                    if(command.NewComments?.Length > 0)
                    {
                        List<BusinnessTaskComment> businnessTaskComments = new List<BusinnessTaskComment>();
                        foreach(CreateBusinnessTaskCommentCommand commentCommand in command.NewComments)
                        {
                            BusinnessTaskComment businnessTaskComment = new BusinnessTaskComment
                            {
                                Date = commentCommand.Date,
                                Text = commentCommand.Text,
                                User = await _applicationDbContext.Users.FirstOrDefaultAsync(x => x.Id == commentCommand.UserId)
                            };

                            businnessTaskComments.Add(businnessTaskComment);
                        }

                        businnessTask.Comments = businnessTaskComments;
                    }

                    if(command.UpdatedComments?.Length > 0)
                    {
                        foreach(UpdateBusinnessTaskCommentCommand updateCommand in command.UpdatedComments)
                        {
                            BusinnessTaskComment businnessTaskComment = await _applicationDbContext.BusinnessTaskComments.FirstOrDefaultAsync(x => x.Id == updateCommand.Id);
                            if(updateCommand.Text is string text)
                            {
                                businnessTaskComment.Text = text;
                            }

                            businnessTaskComment.Date = DateTime.Now;
                            _applicationDbContext.BusinnessTaskComments.Update(businnessTaskComment);
                        }
                    }

                    if(command.NewTaskRealisations?.Length > 0)
                    {
                        List<BusinnessTaskRealisation> businnessTaskRealisations = new List<BusinnessTaskRealisation>();
                        foreach (CreateBusinnessTaskRealisationCommand createCommand in command.NewTaskRealisations)
                        {
                            BusinnessTaskRealisation businnessTaskRealisation = new BusinnessTaskRealisation
                            {
                                Date = createCommand.Date,
                                TimeSpan = createCommand.TimeSpan,
                                Description = createCommand.Description,
                                Employee = await _applicationDbContext.Employees.FirstOrDefaultAsync(x => x.Id == createCommand.EmployeeId),
                            };

                            businnessTaskRealisations.Add(businnessTaskRealisation);
                        }

                        businnessTask.TaskRealisations = businnessTaskRealisations;
                    }

                    if(command.UpdatedTaskRealisations?.Length > 0)
                    {
                        foreach (UpdateBusinnessTaskRealisationCommand updateCommand in command.UpdatedTaskRealisations)
                        {
                            BusinnessTaskRealisation businnessTaskRealisation = await _applicationDbContext.BusinnessTaskRealisations.FirstOrDefaultAsync(x => x.Id == updateCommand.Id);
                            if(updateCommand.Date is DateTime dateTime)
                            {
                                businnessTaskRealisation.Date = dateTime;
                            }

                            if(updateCommand.TimeSpan is TimeSpan timeSpan)
                            {
                                businnessTaskRealisation.TimeSpan = timeSpan;
                            }
                            
                            if (updateCommand.Description is string text)
                            {
                                businnessTaskRealisation.Description = text;
                            }

                            if(updateCommand.EmployeeId is int employeeId)
                            {
                                businnessTaskRealisation.Employee = await _applicationDbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
                            }

                            _applicationDbContext.BusinnessTaskRealisations.Update(businnessTaskRealisation);
                        }
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
