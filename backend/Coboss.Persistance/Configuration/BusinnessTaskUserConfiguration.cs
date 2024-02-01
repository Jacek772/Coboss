using Coboss.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coboss.Persistance.Configuration
{
    public class BusinnessTaskUserConfiguration : IEntityTypeConfiguration<BusinnessTaskEmployee>
    {
        public void Configure(EntityTypeBuilder<BusinnessTaskEmployee> builder)
        {
            builder
                .ToTable("BusinnessTasksUsers", "coboss");

            builder
                .HasKey(x => new { x.BusinnessTaskId, x.EmployeeId });

            builder
                .HasOne(x => x.BusinnessTask)
                .WithMany(x => x.BusinnessTasksEmployees)
                .HasForeignKey(x => x.BusinnessTaskId);

            builder
              .HasOne(x => x.Employee)
              .WithMany(x => x.BusinnessTasksUsers)
              .HasForeignKey(x => x.EmployeeId);
        }
    }
}
