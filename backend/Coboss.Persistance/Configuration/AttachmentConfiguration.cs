using Coboss.Persistance.Configuration.Abstracts;
using Coboss.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Persistance.Configuration
{
    public class AttachmentConfiguration : BaseEntityTypeConfiguration<Attachment>
    {
        public AttachmentConfiguration(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
        {
        }

        public void Configure(EntityTypeBuilder<Attachment> builder)
        {
            builder
                .HasKey(x => x.Id);

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
