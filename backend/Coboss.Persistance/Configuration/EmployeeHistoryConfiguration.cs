using Coboss.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coboss.Persistance.Configuration
{
    internal class EmployeeHistoryConfiguration : IEntityTypeConfiguration<EmployeeHistory>
    {
        public void Configure(EntityTypeBuilder<EmployeeHistory> builder)
        {
            builder
                .ToTable("EmployeeHistories", "coboss");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(x => x.DateFrom)
                .HasDefaultValue(DateTime.MinValue)
                .IsRequired();

            builder
                .Property(x => x.DateTo)
                .HasDefaultValue(DateTime.MaxValue)
                .IsRequired();

            builder
                .Property(x => x.CostHourOfWork)
                .IsRequired();
        }
    }
}
