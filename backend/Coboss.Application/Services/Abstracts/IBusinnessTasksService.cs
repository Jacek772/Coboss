using Coboss.Application.Functions.Commands.BusinnessTasks;
using Coboss.Application.Functions.Query.BusinnessTasks;
using Coboss.Core.Entities;

namespace Coboss.Application.Services.Abstracts
{
    public interface IBusinnessTasksService
    {
        Task<BusinnessTask> GetBusinnessTasksAsync(int id);
        Task<List<BusinnessTask>> GetBusinnessTasksAsync(GetBusinnessTasksQuery query);
        Task CreateBusinnessTaskAsync(BusinnessTask task);
        Task UpdateBusinnessTaskAsync(BusinnessTask businnessTask);
        Task UpdateBusinnessTaskAsync(UpdateBusinnessTaskCommand command);
        Task<bool> BusinnessTasksExistsAsync();
        Task DeleteBusinnessTaskAsync(int id);
        Task DeleteBusinnessTasksAsync(int[] ids);
    }
}
