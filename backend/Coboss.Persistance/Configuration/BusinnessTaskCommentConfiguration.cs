using Coboss.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coboss.Persistance.Configuration
{
    internal class BusinnessTaskCommentConfiguration : IEntityTypeConfiguration<BusinnessTaskComment>
    {
        public void Configure(EntityTypeBuilder<BusinnessTaskComment> builder)
        {
            builder
                .ToTable("BusinnessTaskComments", "coboss");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Text)
                .IsRequired();

            builder
                .Property(x => x.Date)
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();

            // Realtionships
            builder
                .HasOne(x => x.User)
                .WithMany();
        }
    }
}
