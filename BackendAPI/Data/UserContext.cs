using BackendAPI.Models.Database;
using BackendAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Data
{
    public class UserContext : DbContext
    {
        
        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique();
        }
    }
}
