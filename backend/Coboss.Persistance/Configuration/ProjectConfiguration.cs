using Coboss.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Coboss.Persistance.Configuration
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .ToTable("Projects", "coboss");

            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Number)
                .IsRequired();

            builder
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .Property(x => x.Description)
                .IsRequired();

            builder
                .Property(x => x.Term)
                .HasDefaultValue(DateTime.UtcNow)
                .IsRequired();

            // Indexes
            builder
                .HasIndex(x => x.Number)
                .IsUnique();

            // Realtionships
            builder
                .HasOne(x => x.Manager)
                .WithMany();

            builder
                .HasMany(x => x.BusinnessTasks)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId)
                .HasPrincipalKey(x => x.Id);
        }
    }
}
