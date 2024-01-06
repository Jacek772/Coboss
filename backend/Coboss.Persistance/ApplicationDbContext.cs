using Coboss.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Reflection;

namespace Coboss.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        private readonly DatabaseConfiguration _databaseConfiguration;

        public ApplicationDbContext(DbContextOptions options, DatabaseConfiguration databaseConfiguration) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            _databaseConfiguration = databaseConfiguration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeHistory> EmployeeHistories { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<BusinnessTask> BusinnessTasks { get; set; }
        public DbSet<BusinnessTaskRealisation> BusinnessTaskRealisations { get; set; }
        public DbSet<BusinnessTaskComment> BusinnessTaskComments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<GlobalSetting> GlobalSettings { get; set; }
        public DbSet<RefreshTokenData> RefreshTokensData { get; set; }
        public DbSet<ObjectCode> ObjectCodes { get; set; }

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
