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

        public DbSet<Request> Requests { get; set; } 
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Sale> Sales { get; set; }

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

            // Chat Configurations
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("Chats");

                entity.HasOne(c => c.Buyer)
                    .WithMany()
                    .HasForeignKey(c => c.BuyerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Seller)
                    .WithMany()
                    .HasForeignKey(c => c.SellerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Product)
                    .WithMany()
                    .HasForeignKey(c => c.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.Sale)
                    .WithOne(s => s.Chat)
                    .HasForeignKey<Sale>(s => s.ChatId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Message Configurations
            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Messages");

                entity.HasOne(m => m.Chat)
                    .WithMany(c => c.Messages)
                    .HasForeignKey(m => m.ChatId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(m => m.Sender)
                    .WithMany()
                    .HasForeignKey(m => m.SenderId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Sale Configurations
            modelBuilder.Entity<Sale>(entity =>
            {
                entity.ToTable("Sales");

                entity.HasOne(s => s.Buyer)
                    .WithMany()
                    .HasForeignKey(s => s.BuyerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Seller)
                    .WithMany()
                    .HasForeignKey(s => s.SellerId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(s => s.Product)
                    .WithMany()
                    .HasForeignKey(s => s.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // User IDs
            var adminId = Guid.Parse("44147d07-ae1c-45ae-af1c-984ff4d49eba");
            var sellerId = Guid.Parse("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869");
            var buyerId = Guid.Parse("87b84069-9bda-4b44-8d0d-d52c3a8bbc74");

            // Store IDs
            var store1Id = Guid.Parse("44444444-4444-4444-4444-444444444444");
            var store2Id = Guid.Parse("55555555-5555-5555-5555-555555555555");
            var store3Id = Guid.Parse("66666666-6666-6666-6666-666666666666");

            // Category Id
            var electronicsCategoryId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var clothingCategoryId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");
            var furnitureCategoryId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");
            var decorCategoryId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd");

            var request1Id = Guid.Parse("77777777-7777-7777-7777-777777777777");
            var request2Id = Guid.Parse("88888888-8888-8888-8888-888888888888");
            var request3Id = Guid.Parse("99999999-9999-9999-9999-999999999999");


            // Product IDs
            var product1Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890");
            var product2Id = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f23456789012");

            // Chat and Sale IDs
            var chat1Id = Guid.Parse("c1d2e3f4-a5b6-7890-abcd-ef1234567890");
            var sale1Id = Guid.Parse("d2e3f4a5-b6c7-8901-bcde-f23456789012");
            var chat2Id = Guid.Parse("e3f4a5b6-c7d8-9012-cdef-345678901234");
            var sale2Id = Guid.Parse("f4a5b6c7-d8e9-0123-def0-456789012345");

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = adminId, Name = "Admin User", Email = "admin@qatu.com", Role = UserRole.Admin, CreatedAt = DateTime.UtcNow },
                new User { Id = sellerId, Name = "Seller User", Email = "seller@qatu.com",  Role = UserRole.Seller, CreatedAt = DateTime.UtcNow },
                new User { Id = buyerId, Name = "Buyer User", Email = "buyer@qatu.com",Role = UserRole.Buyer, CreatedAt = DateTime.UtcNow }
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

            modelBuilder.Entity<Request>()
                .HasOne(r => r.User)
                .WithOne()
                .HasForeignKey<Request>(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed Requests
            modelBuilder.Entity<Request>().HasData(
                new Request
                {
                    Id = request1Id,
                    UserId = adminId,
                    StoreName = "Admin Store",
                    StoreDescription = "Store managed by admin",
                    Description = "Admin verification request",
                    Status = RequestStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                },
                new Request
                {
                    Id = request2Id,
                    UserId = sellerId,
                    StoreName = "SuperElectro",
                    StoreDescription = "Electronics and gadgets",
                    Description = "Request to update store info",
                    Status = RequestStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                },
                new Request
                {
                    Id = request3Id,
                    UserId = buyerId,
                    StoreName = "Temporary Buyer Store",
                    StoreDescription = "Store created for buyer support request",
                    Description = "Support needed for order issue",
                    Status = RequestStatus.Pending,
                    CreatedAt = DateTime.UtcNow
                }
            );

            // Productos para cada tienda
            modelBuilder.Entity<Product>().HasData(
                // Tech Store
                new Product { Id = product1Id, StoreId = store1Id, Name = "Smartphone", Description = "Latest model smartphone", CategoryId = electronicsCategoryId, Price = 699.99m, Rating = 4.5m, Stock = 50, CreatedAt = DateTime.UtcNow },
                new Product { Id = product2Id, StoreId = store1Id, Name = "Laptop", Description = "High-performance laptop", CategoryId = electronicsCategoryId, Price = 1299.99m, Rating = 4.8m, Stock = 20, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store1Id, Name = "Tablet", Description = "Lightweight and portable", CategoryId = electronicsCategoryId, Price = 499.99m, Rating = 4.3m, Stock = 30, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store1Id, Name = "Smartwatch", Description = "Fitness tracker", CategoryId = electronicsCategoryId, Price = 199.99m, Rating = 4.1m, Stock = 40, CreatedAt = DateTime.UtcNow },
                new Product { Id = new Guid("30a8c927-9774-4057-9f6a-2c4158d2b2d3"), StoreId = store1Id, Name = "Headphones", Description = "Noise-canceling headphones", CategoryId = electronicsCategoryId, Price = 149.99m, Rating = 4.4m, Stock = 25, CreatedAt = DateTime.UtcNow },

                // Fashion Store
                new Product { Id = new Guid("377f3896-ed4d-4e68-ad30-02cc65e426b8"), StoreId = store2Id, Name = "Jeans", Description = "Blue denim jeans", CategoryId = clothingCategoryId, Price = 49.99m, Rating = 4.2m, Stock = 100, CreatedAt = DateTime.UtcNow },
                new Product { Id = new Guid("45726569-0a33-4ed1-9ce3-072324033594"), StoreId = store2Id, Name = "T-Shirt", Description = "100% Cotton T-shirt", CategoryId = clothingCategoryId, Price = 19.99m, Rating = 4.0m, Stock = 150, CreatedAt = DateTime.UtcNow },
                new Product { Id = new Guid("73e0fe7b-530b-4cc1-9ad0-a028f116b0a8"), StoreId = store2Id, Name = "Jacket", Description = "Waterproof winter jacket", CategoryId = clothingCategoryId, Price = 99.99m, Rating = 4.3m, Stock = 60, CreatedAt = DateTime.UtcNow },
                new Product { Id = new Guid("7a44753c-aa8c-480f-ade8-bcf20ab8a6ce"), StoreId = store2Id, Name = "Sneakers", Description = "Comfortable sneakers", CategoryId = clothingCategoryId, Price = 59.99m, Rating = 4.2m, Stock = 120, CreatedAt = DateTime.UtcNow },

                // Home Store
                new Product { Id = new Guid("81461fab-579e-4b88-8406-8c66890f59a3"), StoreId = store3Id, Name = "Sofa", Description = "Modern 3-seater sofa", CategoryId = furnitureCategoryId, Price = 899.99m, Rating = 4.6m, Stock = 10, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store3Id, Name = "Dining Table", Description = "Solid wood dining table", CategoryId = furnitureCategoryId, Price = 499.99m, Rating = 4.5m, Stock = 15, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store3Id, Name = "Bed Frame", Description = "Queen size bed frame", CategoryId = furnitureCategoryId, Price = 299.99m, Rating = 4.7m, Stock = 5, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store3Id, Name = "Lamp", Description = "LED floor lamp", CategoryId = furnitureCategoryId, Price = 49.99m, Rating = 4.2m, Stock = 30, CreatedAt = DateTime.UtcNow },
                new Product { Id = Guid.NewGuid(), StoreId = store3Id, Name = "Carpet", Description = "Soft area rug", CategoryId = furnitureCategoryId, Price = 99.99m, Rating = 4.3m, Stock = 20, CreatedAt = DateTime.UtcNow }
            );

            // Seed Chats
            modelBuilder.Entity<Chat>().HasData(
                new { Id = chat1Id, BuyerId = buyerId, SellerId = sellerId, ProductId = product1Id, CreatedAt = DateTime.UtcNow },
                new { Id = chat2Id, BuyerId = buyerId, SellerId = sellerId, ProductId = product2Id, CreatedAt = DateTime.UtcNow }
            );

            // Seed Sales
            modelBuilder.Entity<Sale>().HasData(
                new { Id = sale1Id, ChatId = chat1Id, BuyerId = buyerId, SellerId = sellerId, ProductId = product1Id, Status = SaleStatus.Waiting, CreatedAt = DateTime.UtcNow },
                new { Id = sale2Id, ChatId = chat2Id, BuyerId = buyerId, SellerId = sellerId, ProductId = product2Id, Status = SaleStatus.Pending, CreatedAt = DateTime.UtcNow }
            );

            // Seed Messages
            modelBuilder.Entity<Message>().HasData(
                new { Id = Guid.NewGuid(), ChatId = chat1Id, SenderId = buyerId, Content = "Hi, is the smartphone still in stock?", SentAt = DateTime.UtcNow },
                new { Id = Guid.NewGuid(), ChatId = chat1Id, SenderId = sellerId, Content = "Yes, we have 50 units available!", SentAt = DateTime.UtcNow.AddMinutes(5) },
                new { Id = Guid.NewGuid(), ChatId = chat2Id, SenderId = buyerId, Content = "Can you tell me more about the laptop?", SentAt = DateTime.UtcNow },
                new { Id = Guid.NewGuid(), ChatId = chat2Id, SenderId = sellerId, Content = "It has 16GB RAM and a 1TB SSD.", SentAt = DateTime.UtcNow.AddMinutes(5) }
            );
        }
    }
}
