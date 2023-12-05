using Coboss.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coboss.Persistance.Configuration
{
    public class GlobalSettingConfiguration : IEntityTypeConfiguration<GlobalSetting>
    {
        public void Configure(EntityTypeBuilder<GlobalSetting> builder)
        {
            builder
                .ToTable("GlobalSettings", "coboss");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Key)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.Value)
                .IsRequired();

            builder
                .Property(x => x.Type)
                .IsRequired();

            // Indexes
            builder
                .HasIndex(x => x.Key)
                .IsUnique();
        }
    }
}
