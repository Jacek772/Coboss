using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class GlobalSetting : BaseEntitiy
    {
        public string Key { get; set; } = default!;
        public string Value { get; set; } = default!;
        public string Type { get; set; } = default!;
    }
}