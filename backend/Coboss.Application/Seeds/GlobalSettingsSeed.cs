using Coboss.Application.Seeds.abstracts;
using Coboss.Application.Services.Abstracts;
using static Coboss.Core.Entities.GlobalSetting;

namespace Coboss.Application.Seeds
{
    public class GlobalSettingsSeed : ISeed
    {
        private readonly IGlobalSettingsService _globalSettingsService;

        public GlobalSettingsSeed(IGlobalSettingsService globalSettingsService)
        {
            _globalSettingsService = globalSettingsService;
        }

        public async Task Seed()
        {
            await _globalSettingsService.SetGlobalSettingValueAsync(GlobalSettingKey.EmployeeCodeLength, 7);
        }
    }
}
