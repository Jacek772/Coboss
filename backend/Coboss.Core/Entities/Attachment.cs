using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class Attachment : BaseEntitiy
    {
        public string FileName { get; set; } = default!;
        public string FilePath { get; set; } = default!;
    }
}
