using Microsoft.EntityFrameworkCore;
using StandardProcedureAPI.Models;

namespace StandardProcedureAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<features> Features { get; set; }
        public DbSet<how_to_use_steps> HowToUseSteps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Optional: configure default schema, table names, etc.
            base.OnModelCreating(modelBuilder);
        }
    }
}
