using Coboss.Persistance.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Coboss.Persistance.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("Users", "coboss");

            builder
                .HasKey(x => x.ID);

            builder
                .Property(x => x.Login)
                .IsRequired()
                .HasMaxLength(25);

            builder
                .Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
               .Property(x => x.Salt)
               .IsRequired()
               .HasMaxLength(255);

            // Indexes
            builder
                .HasIndex(x => x.Login)
                .IsUnique();
        }
    }
}
