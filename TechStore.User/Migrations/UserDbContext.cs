using Microsoft.EntityFrameworkCore;

namespace TechStore.User.Migrations
{
    public class UserDbContext : DbContext
    {
        public UserDbContext() { }
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }
        public DbSet<Models.User> Users { get; set; }
        public DbSet<Models.Order> Orders { get; set; }
        public DbSet<Models.OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Models.User>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Models.Order>()
               .HasKey(x => x.Id);
            modelBuilder.Entity<Models.OrderDetail>()
               .HasKey(x => x.Id);

            modelBuilder.Entity<Models.User>().HasMany(e => e.Orders)
                .WithOne(e => e.User);
            modelBuilder.Entity<Models.Order>().HasMany(e => e.OrderDetails)
                .WithOne(e => e.Order).OnDelete(DeleteBehavior.Cascade);
        }

    }
}
