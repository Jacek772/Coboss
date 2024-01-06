using Coboss.Application.Extensions;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Core.Entities.Abstracts;
using Coboss.Persistance;
using Coboss.Types.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Coboss.Application.Services
{
    public class ObjectCodesService : IObjectCodesService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ObjectCodesService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<ObjectCode> GetNewObjectCode<T>(int codeLength)
            where T : BaseEntitiy
        {
            ObjectCode objectCodeLast = await _applicationDbContext.ObjectCodes
                .OrderByDescending(x => x.CodeNumber)
                .Where(x => x.EntityName == typeof(T).Name)
                .FirstOrDefaultAsync();

            ObjectCode objectCode;
            if (objectCodeLast == null)
            {
                objectCode = new ObjectCode
                {
                    CodeNumber = 1,
                    CodeLength = codeLength,
                    EntityName = typeof(T).Name
                };
            }
            else
            {
                objectCode = new ObjectCode
                {
                    CodeNumber = objectCodeLast.CodeNumber + 1,
                    CodeLength = codeLength,
                    EntityName = typeof(T).Name
                };
            }

            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    await _applicationDbContext.ObjectCodes.AddAsync(objectCode);
                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new BadRequestException($"ObjectCode create error\n{ex.ToMessage()}");
                }
            }
            return objectCode;
        }
    }
}
