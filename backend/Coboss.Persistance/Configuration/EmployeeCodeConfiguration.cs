using Coboss.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coboss.Persistance.Configuration
{
    public class EmployeeCodeConfiguration : IEntityTypeConfiguration<EmployeeCode>
    {
        public void Configure(EntityTypeBuilder<EmployeeCode> builder)
        {
            builder
                .ToTable("EmployeeCodes", "coboss");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.CodeNumber)
                .IsRequired();

            builder
                .Property(x => x.CodeLength)
                .IsRequired()
                .HasDefaultValue(10);

            builder
                .Ignore(x => x.Code);

            // Indexes
            builder
                .HasIndex(x => x.CodeNumber)
                .IsUnique();
        }
    }
}
