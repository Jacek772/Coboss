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
                .ToTable("Employers", "coboss");

            builder
                .HasKey(x => x.ID);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(100);

            // Realtionships
            builder
                .HasOne(x => x.User)
                .WithOne(x => x.Employee)
                .HasForeignKey<User>(x => x.EmployeeID);
        }
    }
}
