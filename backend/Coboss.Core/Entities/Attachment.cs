using Coboss.Core.Entities.Abstracts;

namespace Coboss.Core.Entities
{
    public class Attachment : BaseEntitiy
    {
        public enum AttachmentFileType
        {
            TXT,
            PDF,
            DOCX,
            JPG,
            PNG
        }

        public string TableName { get; set; } = default!;
        public Guid ParentID { get; set; }
        public string FileName { get; set; } = default!;
        public string FilePath { get; set; } = default!;
        public AttachmentFileType FileType { get; set; } = default!;
    }
}
