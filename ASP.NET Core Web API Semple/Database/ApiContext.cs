using ASP.NET_Core_Web_API_Semple.Models.Laptop;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_Web_API_Semple.Database
{
    public class ApiContext : DbContext
    {
        public DbSet<LaptopSpecification> Laptops => Set<LaptopSpecification>();
        public ApiContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Laptops.db");
        }
    }
}
