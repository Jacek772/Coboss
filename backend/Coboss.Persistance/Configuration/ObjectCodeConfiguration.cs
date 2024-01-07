using Coboss.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coboss.Persistance.Configuration
{
    public class ObjectCodeConfiguration : IEntityTypeConfiguration<ObjectCode>
    {
        public void Configure(EntityTypeBuilder<ObjectCode> builder)
        {
            builder
                .ToTable("ObjectCodes", "coboss");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.CodeNumber)
                .IsRequired();

            builder
                .Property(x => x.CodeLength)
                .IsRequired()
                .HasDefaultValue(10);

            builder
                .Ignore(x => x.Code);

            // Indexes
            builder
                .HasIndex(x => new { x.EntityName, x.CodeNumber })
                .IsUnique();
        }
    }
}
