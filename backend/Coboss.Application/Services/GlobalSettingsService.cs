using Coboss.Application.Extensions;
using Coboss.Application.Services.Abstracts;
using Coboss.Core.Entities;
using Coboss.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using static Coboss.Core.Entities.GlobalSetting;

namespace Coboss.Application.Services
{
    public class GlobalSettingsService : IGlobalSettingsService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GlobalSettingsService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> GetValueIntAsync(GlobalSettingKey key)
        {
            GlobalSetting globalSetting = await _applicationDbContext.GlobalSettings.FirstOrDefaultAsync(x => x.Key == key);
            return globalSetting?.GetValueInt() ?? 0;
        }

        public async Task<string> GetValueStringAsync(GlobalSettingKey key)
        {
            GlobalSetting globalSetting = await _applicationDbContext.GlobalSettings.FirstOrDefaultAsync(x => x.Key == key);
            return globalSetting?.GetValueString();
        }

        public async Task SetValueAsync<T>(GlobalSettingKey key, T value)
        {
            using (IDbContextTransaction transaction = _applicationDbContext.Database.BeginTransaction())
            {
                try
                {
                    GlobalSetting globalSetting = await _applicationDbContext.GlobalSettings.FirstOrDefaultAsync(x => x.Key == key);
                    if(globalSetting != null)
                    {
                        globalSetting.SetValue(value);
                        _applicationDbContext.GlobalSettings.Update(globalSetting);
                    }
                    else
                    {
                        globalSetting = new GlobalSetting
                        {
                            Key = key,
                        };
                        globalSetting.SetValue(value);
                        _applicationDbContext.GlobalSettings.Add(globalSetting);
                    }

                    await _applicationDbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Employee create error\n{ex.ToMessage()}");
                }
            }
        }
    }
}
