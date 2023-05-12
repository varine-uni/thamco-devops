using Microsoft.EntityFrameworkCore;

namespace ThAmCo.Web.Data
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }
    }
}
