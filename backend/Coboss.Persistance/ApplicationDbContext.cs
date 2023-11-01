using Coboss.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Coboss.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        private readonly DatabaseConfiguration _databaseConfiguration;

        public ApplicationDbContext(DbContextOptions options, DatabaseConfiguration databaseConfiguration) : base(options)
        {
            _databaseConfiguration = databaseConfiguration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Employee> Employers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<BusinnessTask> BusinnessTasks { get; set; }
        public DbSet<BusinnessTaskRealisation> BusinnessTaskRealisations { get; set; }
        public DbSet<Attachment> Attachments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(_databaseConfiguration.ConnectionString,
                options => options.MigrationsHistoryTable("__EFMigrationsHistory", "coboss"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}