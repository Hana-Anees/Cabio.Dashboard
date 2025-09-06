using Cabio.Dashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cabio.Dashboard.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Driver -> Vehicle (1:M)
            modelBuilder.Entity<Driver>()
                .HasMany(d => d.Vehicles)
                .WithOne(v => v.Driver)
                .HasForeignKey(v => v.DriverId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
