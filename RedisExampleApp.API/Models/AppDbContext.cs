using Microsoft.EntityFrameworkCore;

namespace RedisExampleApp.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product() { Id = 1, Name = "Kalem", Price = 10 },
                new Product() { Id = 2, Name = "Kitap", Price = 20 },
                new Product() { Id = 3, Name = "Defter", Price = 30 }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
