using Coboss.Persistance.Configuration.Abstracts;
using Coboss.Persistance.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Persistance.Configuration
{
    public class EmployeeConfiguration : BaseEntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
        {
        }

        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(x => x.Surname)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
