using Coboss.Persistance.Entities;
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
                .HasKey(x => x.ID);

            builder
                .Property(x => x.Date)
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();

            builder
                .Property(x => x.TimeSpan)
                .IsRequired();
        }
    }
}
