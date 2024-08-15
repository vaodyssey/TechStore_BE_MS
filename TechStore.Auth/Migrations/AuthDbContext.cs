using Microsoft.EntityFrameworkCore;
using TechStore.Auth.Models;

namespace TechStore.Auth.Migrations
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext() { }
        public AuthDbContext(DbContextOptions<AuthDbContext> options): base(options){ }
        public DbSet<User> Users { get; set; }
     
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);
        }

    }
}
