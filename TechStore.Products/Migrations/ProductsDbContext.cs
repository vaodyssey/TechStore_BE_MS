using Microsoft.EntityFrameworkCore;
using TechStore.Products.Models;

namespace TechStore.Products.Migrations
{
    public class ProductsDbContext:DbContext
    {
        public ProductsDbContext() { }
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) { }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Brand> Brands{ get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Brand>().HasKey(x => x.Id);
            modelBuilder.Entity<Brand>().HasMany(e => e.Products)
                .WithOne(e => e.Brand).IsRequired();
        }
    }
}
