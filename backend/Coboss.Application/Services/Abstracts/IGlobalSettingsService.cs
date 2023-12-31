using static Coboss.Core.Entities.GlobalSetting;

namespace Coboss.Application.Services.Abstracts
{
    public interface IGlobalSettingsService
    {
        Task SetGlobalSettingValueAsync<T>(GlobalSettingKey key, T value);
        Task<string> GetGlobalSettingValueStringAsync(GlobalSettingKey key);
        Task<int> GetGlobalSettingValueIntAsync(GlobalSettingKey key);
    }
}
