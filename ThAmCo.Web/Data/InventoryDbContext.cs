using Microsoft.EntityFrameworkCore;
using ThAmCo.Web.Models;

namespace ThAmCo.Web.Data
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
