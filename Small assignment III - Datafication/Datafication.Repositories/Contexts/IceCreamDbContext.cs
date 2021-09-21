using Microsoft.EntityFrameworkCore;
using Datafication.Repositories.Entities;

namespace Datafication.Repositories.Contexts
{
    public class IceCreamDbContext : DbContext
    {
        public IceCreamDbContext(DbContextOptions<IceCreamDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {}

        public DbSet<Category> Categories { get; set; }
        public DbSet<IceCream> IceCreams { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        
    }
}
