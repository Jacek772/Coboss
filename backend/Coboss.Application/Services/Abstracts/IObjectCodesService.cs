using Coboss.Core.Entities.Abstracts;
using Coboss.Core.Entities;

namespace Coboss.Application.Services.Abstracts
{
    public interface IObjectCodesService
    {
        Task<ObjectCode> GetNewObjectCode<T>(int codeLength)
            where T : BaseEntitiy;
    }
}
