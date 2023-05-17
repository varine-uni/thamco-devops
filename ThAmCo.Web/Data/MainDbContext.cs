using Microsoft.EntityFrameworkCore;

namespace ThAmCo.Web.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {
        }
    }
}
