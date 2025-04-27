using Microsoft.EntityFrameworkCore;
using Qatu.Domain.Entities;
using Qatu.Domain.Enums;

namespace Qatu.Infrastructure.Persistence
{
    public class QatuDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Store> Stores { get; set; }

        public QatuDbContext(DbContextOptions<QatuDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string password = "password123";

            base.OnModelCreating(modelBuilder);

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Admin User",
                    Email = "admin@qatu.com",
                    Password = password,
                    Role = UserRole.Admin,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = 2,
                    Name = "Seller User",
                    Email = "seller@qatu.com",
                    Password = password,
                    Role = UserRole.Seller,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = 3,
                    Name = "Buyer User",
                    Email = "buyer@qatu.com",
                    Password = password,
                    Role = UserRole.Buyer,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed Stores
            modelBuilder.Entity<Store>().HasData(
                new Store
                {
                    Id = 1,
                    UserId = 2,
                    Name = "Tech Store",
                    Description = "Electronics and gadgets",
                    CreatedAt = DateTime.UtcNow
                },
                new Store
                {
                    Id = 2,
                    UserId = 2,
                    Name = "Fashion Store",
                    Description = "Clothing and accessories",
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    StoreId = 1,
                    Name = "Smartphone",
                    Description = "Latest model smartphone",
                    Category = "Electronics",
                    Price = 699.99m,
                    Rating = 4.5m,
                    Stock = 50,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 2,
                    StoreId = 1,
                    Name = "Laptop",
                    Description = "High-performance laptop",
                    Category = "Electronics",
                    Price = 1299.99m,
                    Rating = 4.8m,
                    Stock = 20,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Id = 3,
                    StoreId = 2,
                    Name = "Jeans",
                    Description = "Blue denim jeans",
                    Category = "Clothing",
                    Price = 49.99m,
                    Rating = 4.2m,
                    Stock = 100,
                    CreatedAt = DateTime.UtcNow
                }
            );
        }
    }
}
