using Cabio.Dashboard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cabio.Dashboard.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }        
    }
}
