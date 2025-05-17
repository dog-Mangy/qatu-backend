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
        public DbSet<Category> Categories { get; set; }

        public QatuDbContext(DbContextOptions<QatuDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string password = "password123";

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany() 
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.StoreId).HasColumnName("store_id");
            });

            // User IDs
            var adminId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var sellerId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var buyerId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            // Store IDs
            var store1Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var store2Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var store3Id = Guid.Parse("66666666-6666-6666-6666-666666666666");

            // Category Id
            var electronicsCategoryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var clothingCategoryId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var furnitureCategoryId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
            var decorCategoryId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = adminId, Name = "Admin User", Email = "admin@qatu.com", Password = password, Role = UserRole.Admin, CreatedAt = DateTime.UtcNow },
                new User { Id = sellerId, Name = "Seller User", Email = "seller@qatu.com", Password = password, Role = UserRole.Seller, CreatedAt = DateTime.UtcNow },
                new User { Id = buyerId, Name = "Buyer User", Email = "buyer@qatu.com", Password = password, Role = UserRole.Buyer, CreatedAt = DateTime.UtcNow }
            );

            // Seed Stores
            modelBuilder.Entity<Store>().HasData(
                new Store { Id = store1Id, UserId = sellerId, Name = "Tech Store", Description = "Electronics and gadgets", CreatedAt = DateTime.UtcNow },
                new Store { Id = store2Id, UserId = sellerId, Name = "Fashion Store", Description = "Clothing and accessories", CreatedAt = DateTime.UtcNow },
                new Store { Id = store3Id, UserId = sellerId, Name = "Home Store", Description = "Home essentials and furniture", CreatedAt = DateTime.UtcNow }
            );

            
            // Seed Categorys
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = electronicsCategoryId, Name = "Electronics", CreatedAt = DateTime.UtcNow },
                new Category { Id = clothingCategoryId, Name = "Clothing", CreatedAt = DateTime.UtcNow },
                new Category { Id = furnitureCategoryId, Name = "Furniture", CreatedAt = DateTime.UtcNow },
                new Category { Id = decorCategoryId, Name = "Home Decor", CreatedAt = DateTime.UtcNow }
            );


            // Productos para cada tienda
            modelBuilder.Entity<Product>().HasData(
                // Tech Store
                new Product { Id = Guid.NewGuid(), StoreId = store1Id, Name = "Smartphone", Description = "Latest model smartphone", CategoryId = electronicsCategoryId, Price = 699.99m, Rating = 4.5m, Stock = 50, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store1Id, Name = "Laptop", Description = "High-performance laptop", CategoryId = electronicsCategoryId, Price = 1299.99m, Rating = 4.8m, Stock = 20, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store1Id, Name = "Tablet", Description = "Lightweight and portable", CategoryId = electronicsCategoryId, Price = 499.99m, Rating = 4.3m, Stock = 30, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store1Id, Name = "Smartwatch", Description = "Fitness tracker", CategoryId = electronicsCategoryId, Price = 199.99m, Rating = 4.1m, Stock = 40, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store1Id, Name = "Headphones", Description = "Noise-canceling headphones", CategoryId = electronicsCategoryId, Price = 149.99m, Rating = 4.4m, Stock = 25, CreatedAt = DateTime.UtcNow },

                // Fashion Store
                new Product { Id = Guid.NewGuid(), StoreId = store2Id, Name = "Jeans", Description = "Blue denim jeans", CategoryId = clothingCategoryId, Price = 49.99m, Rating = 4.2m, Stock = 100, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store2Id, Name = "T-Shirt", Description = "100% Cotton T-shirt", CategoryId = clothingCategoryId, Price = 19.99m, Rating = 4.0m, Stock = 150, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store2Id, Name = "Jacket", Description = "Waterproof winter jacket", CategoryId = clothingCategoryId, Price = 99.99m, Rating = 4.3m, Stock = 60, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store2Id, Name = "Sneakers", Description = "Comfortable sneakers", CategoryId = clothingCategoryId, Price = 59.99m, Rating = 4.2m, Stock = 120, CreatedAt = DateTime.UtcNow },

                // Home Store
                new Product { Id = Guid.NewGuid(), StoreId = store3Id, Name = "Sofa", Description = "Modern 3-seater sofa", CategoryId = furnitureCategoryId, Price = 899.99m, Rating = 4.6m, Stock = 10, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store3Id, Name = "Dining Table", Description = "Solid wood dining table", CategoryId = furnitureCategoryId, Price = 499.99m, Rating = 4.5m, Stock = 15, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store3Id, Name = "Bed Frame", Description = "Queen size bed frame", CategoryId = furnitureCategoryId, Price = 299.99m, Rating = 4.7m, Stock = 5, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store3Id, Name = "Lamp", Description = "LED floor lamp", CategoryId = furnitureCategoryId, Price = 49.99m, Rating = 4.2m, Stock = 30, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store3Id, Name = "Carpet", Description = "Soft area rug", CategoryId = furnitureCategoryId, Price = 99.99m, Rating = 4.3m, Stock = 20, CreatedAt = DateTime.UtcNow }
            );
        }
    }
}
