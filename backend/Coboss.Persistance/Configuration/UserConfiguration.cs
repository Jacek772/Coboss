using Coboss.Persistance.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Coboss.Persistance.Configuration.Abstracts;

namespace Coboss.Persistance.Configuration
{
    internal class UserConfiguration : BaseEntityTypeConfiguration<User>
    {
        public UserConfiguration(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
        {
        }

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Login)
                .IsRequired()
                .HasMaxLength(25);

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
                .HasIndex(x => x.Login)
                .IsUnique();

            // Realtionships
            builder
                .HasOne(x => x.Employee)
                .WithOne(x => x.User);
        }
    }
}
