using Coboss.Persistance.Configuration.Abstracts;
using Coboss.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coboss.Persistance.Configuration
{
    public class BusinnessTaskConfiguration : BaseEntityTypeConfiguration<BusinnessTask>
    {
        public BusinnessTaskConfiguration(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
        {
        }

        public void Configure(EntityTypeBuilder<BusinnessTask> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            builder
                .Property(x => x.Description)
                .IsRequired();

            builder
                .Property(x => x.Date)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder
                .Property(x => x.Term)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            // Realtionships
            builder
                .HasMany(x => x.BusinnessTaskRealisations)
                .WithOne();

            builder
                .HasMany(x => x.Attachments)
                .WithOne();
        }
    }
}
