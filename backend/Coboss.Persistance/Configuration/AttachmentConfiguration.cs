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
                .HasKey(x => x.ID);

            builder
                .Property(x => x.FileName)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.FilePath)
                .IsRequired();
        }
    }
}
