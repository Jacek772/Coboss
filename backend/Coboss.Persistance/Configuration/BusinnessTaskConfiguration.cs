﻿using Coboss.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coboss.Persistance.Configuration
{
    public class BusinnessTaskConfiguration : IEntityTypeConfiguration<BusinnessTask>
    {
        public void Configure(EntityTypeBuilder<BusinnessTask> builder)
        {
            builder
                .ToTable("BusinnessTasks", "coboss");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            builder
                .Property(x => x.Description)
                .IsRequired();

            builder
                .Property(x => x.Date)
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();

            builder
                .Property(x => x.Term)
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();

            // Realtionships
            builder
                .HasMany(x => x.TaskRealisations)
                .WithOne(x => x.Task)
                .HasForeignKey(x => x.TaskId)
                .HasPrincipalKey(x => x.Id);

            builder
                .HasMany(x => x.Comments)
                .WithOne(x => x.Task)
                .HasForeignKey(x => x.TaskId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
