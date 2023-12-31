using Coboss.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coboss.Persistance.Configuration
{
    public class BusinnessTaskRealisationConfiguration : IEntityTypeConfiguration<BusinnessTaskRealisation>
    {
        public void Configure(EntityTypeBuilder<BusinnessTaskRealisation> builder)
        {
            builder
                .ToTable("BusinnessTaskRealisations", "coboss");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Date)
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();

            builder
                .Property(x => x.TimeSpan)
                .HasDefaultValue(TimeSpan.Zero)
                .IsRequired();
        }
    }
}
