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
    public class BusinnessTaskRealisationConfiguration : BaseEntityTypeConfiguration<BusinnessTaskRealisation>
    {
        public BusinnessTaskRealisationConfiguration(DatabaseConfiguration databaseConfiguration) : base(databaseConfiguration)
        {
        }

        public void Configure(EntityTypeBuilder<BusinnessTaskRealisation> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Date)
                .HasDefaultValue(DateTime.Now)
                .IsRequired();

            builder
                .Property(x => x.TimeSpan)
                .IsRequired();
        }
    }
}
