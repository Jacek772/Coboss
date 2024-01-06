using Coboss.Application.Functions.Commands.BusinnessTaskRealisations;
using Coboss.Application.Functions.Query.BusinnessTaskRealisations;
using Coboss.Core.Entities;

namespace Coboss.Application.Services.Abstracts
{
    public interface IBusinnessTaskRealisationsService
    {
        Task<List<BusinnessTaskRealisation>> GetAsync(GetBusinnessTaskRealisationsQuery query);
        Task CreateAsync(BusinnessTaskRealisation taskRealisation);
        Task UpdateAsync(UpdateBusinnessTaskRealisationCommand command);
        Task<bool> ExistsAsync();
        Task DeleteAsync(int[] ids);
    }
}
