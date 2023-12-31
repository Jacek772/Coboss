using Coboss.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coboss.Persistance.Configuration
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .ToTable("Employees", "coboss");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(100);

            // Indexes
            //builder
            //    .HasIndex(x => x.NIP)
            //    .IsUnique();

            //builder
            //    .HasIndex(x => x.PESEL)
            //    .IsUnique();

            // Realtionships
            builder
                .HasOne(x => x.User)
                .WithOne(x => x.Employee)
                .HasForeignKey<User>(x => x.EmployeeId);

            builder
                .HasMany(x => x.EmployeeHistories)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
