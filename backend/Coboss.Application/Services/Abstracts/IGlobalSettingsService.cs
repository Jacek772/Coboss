using static Coboss.Core.Entities.GlobalSetting;

namespace Coboss.Application.Services.Abstracts
{
    public interface IGlobalSettingsService
    {
        Task SetValueAsync<T>(GlobalSettingKey key, T value);
        Task<string> GetValueStringAsync(GlobalSettingKey key);
        Task<int> GetValueIntAsync(GlobalSettingKey key);
    }
}
