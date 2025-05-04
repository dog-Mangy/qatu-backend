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

            var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var sellerId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var buyerId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            var store1Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var store2Id = Guid.Parse("55555555-5555-5555-5555-555555555555");

            var product1Id = Guid.Parse("66666666-6666-6666-6666-666666666666");
            var product2Id = Guid.Parse("77777777-7777-7777-7777-777777777777");
            var product3Id = Guid.Parse("88888888-8888-8888-8888-888888888888");

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminId,
                    Name = "Admin User",
                    Email = "admin@qatu.com",
                    Password = password,
                    Role = UserRole.Admin,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = sellerId,
                    Name = "Seller User",
                    Email = "seller@qatu.com",
                    Password = password,
                    Role = UserRole.Seller,
                    CreatedAt = DateTime.UtcNow
                },
                new User
                {
                    Id = buyerId,
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
                    Id = store1Id,
                    UserId = sellerId,
                    Name = "Tech Store",
                    Description = "Electronics and gadgets",
                    CreatedAt = DateTime.UtcNow
                },
                new Store
                {
                    Id = store2Id,
                    UserId = sellerId,
                    Name = "Fashion Store",
                    Description = "Clothing and accessories",
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Seed Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = product1Id,
                    StoreId = store1Id,
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
                    Id = product2Id,
                    StoreId = store1Id,
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
                    Id = product3Id,
                    StoreId = store2Id,
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