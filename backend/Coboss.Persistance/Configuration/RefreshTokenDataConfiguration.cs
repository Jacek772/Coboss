using Coboss.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coboss.Persistance.Configuration
{
    public class RefreshTokenDataConfiguration : IEntityTypeConfiguration<RefreshTokenData>
    {
        public void Configure(EntityTypeBuilder<RefreshTokenData> builder)
        {
            builder
                .ToTable("RefreshTokensData", "coboss");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Token)
                .IsRequired()
                .HasMaxLength(88);

            builder
                .Property(x => x.CreationDate)
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();

            builder
                .Property(x => x.ExpiryDate)
                .IsRequired();

            // Indexes
            builder
                .HasIndex(x => x.Token);

            // Realtionships
            builder
                .HasOne(x => x.User)
                .WithMany();
        }
    }
}
