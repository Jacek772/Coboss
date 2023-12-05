using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Coboss.Core.Entities;

namespace Coboss.Persistance.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("Users", "coboss");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder
               .Property(x => x.Salt)
               .IsRequired()
               .HasMaxLength(255);

            // Indexes
            builder
                .HasIndex(x => x.Email)
                .IsUnique();

            // Realtionships
            builder
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId);
        }
    }
}
