using Microsoft.EntityFrameworkCore;
using WebAPIShirt.Model;

namespace WebAPIShirt.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Shirt> Shirts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<Shirt>().HasData(
                new Shirt { ShirtId = 1, Brand = "MyBrand", Color = "Blue", Gender = "Men", Price = 30, Size = 10 },
                new Shirt { ShirtId = 2, Brand = "MyBrand", Color = "Black", Gender = "Men", Price = 35, Size = 12 },
                new Shirt { ShirtId = 3, Brand = "Your Brand", Color = "Pink", Gender = "Women", Price = 28, Size = 8 },
                new Shirt { ShirtId = 4, Brand = "Your Brand", Color = "Yello", Gender = "Women", Price = 30, Size = 9 }
            );
        }
    }
}
