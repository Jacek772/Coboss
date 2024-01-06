using Coboss.Application.Functions.Commands.BusinnessTaskComments;
using Coboss.Application.Functions.Query.BusinnessTaskComments;
using Coboss.Core.Entities;

namespace Coboss.Application.Services.Abstracts
{
    public interface IBusinnessTaskCommentsService
    {
        Task<List<BusinnessTaskComment>> GetAsync(GetBusinnessTaskCommentsQuery query);
        Task CreateAsync(BusinnessTaskComment taskComment);
        Task UpdateAsync(BusinnessTaskComment businnessTaskComment);
        Task UpdateAsync(UpdateBusinnessTaskCommentCommand command);
        Task<bool> ExistsAsync();
        Task DeletesAsync(int[] ids);
    }
}
