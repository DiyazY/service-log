using Microsoft.EntityFrameworkCore;

namespace sl.infrastructure.Repositories.Context
{
    public sealed class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options)
        : base(options)
        { }

        public DbSet<LogEntity> Logs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.ApplyLogConfigurations();
        }
    }
}
