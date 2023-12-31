using Coboss.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coboss.Persistance.Configuration
{
    public class AttachmentConfiguration : IEntityTypeConfiguration<Attachment>
    {
        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder
                .ToTable("Attachments", "coboss");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.TableName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.ParentID)
                .IsRequired();

            builder
                .Property(x => x.FileName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.FilePath)
                .IsRequired();

            builder
                .Property(x => x.FileType)
                .HasDefaultValue(Attachment.AttachmentFileType.TXT)
                .IsRequired();

            // Indexes
            builder
                .HasIndex(x => new { x.TableName, x.ParentID })
                .IsUnique();
        }
    }
}
