using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qatu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatev4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ImageUrl = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    StoreName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StoreDescription = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stores_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    store_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CategoryId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Stores_store_id",
                        column: x => x.store_id,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BuyerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SellerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chats_Users_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chats_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ChatId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SenderId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Content = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SentAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ChatId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    BuyerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    SellerId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProductId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sales_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Users_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Users_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(73), null, null, "Electronics", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(76), null, null, "Clothing", null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(82), null, null, "Furniture", null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null, new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(85), null, null, "Home Decor", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 5, 31, 15, 14, 15, 463, DateTimeKind.Utc).AddTicks(9797), "admin@qatu.com", "Admin User", "password123", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 5, 31, 15, 14, 15, 463, DateTimeKind.Utc).AddTicks(9802), "seller@qatu.com", "Seller User", "password123", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 5, 31, 15, 14, 15, 463, DateTimeKind.Utc).AddTicks(9804), "buyer@qatu.com", "Buyer User", "password123", 1 }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "CreatedAt", "Description", "Status", "StoreDescription", "StoreName", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1573), "Admin verification request", 0, "Store managed by admin", "Admin Store", null, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1576), "Request to update store info", 0, "Electronics and gadgets", "SuperElectro", null, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1579), "Support needed for order issue", 0, "Store created for buyer support request", "Temporary Buyer Store", null, new Guid("33333333-3333-3333-3333-333333333333") }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(15), "Electronics and gadgets", "Tech Store", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(19), "Clothing and accessories", "Fashion Store", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(22), "Home essentials and furniture", "Home Store", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "created_at", "Description", "Name", "Price", "Rating", "Stock", "store_id" },
                values: new object[,]
                {
                    { new Guid("100465f6-fb9d-4422-86be-3771df5ed44a"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1666), "Comfortable sneakers", "Sneakers", 59.99m, 4.2m, 120, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("1b898f51-e2f7-4f5e-aa13-135761aa0268"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1678), "Solid wood dining table", "Dining Table", 499.99m, 4.5m, 15, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("3244ecd7-6064-4d45-bed1-1b439b3391fc"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1646), "Noise-canceling headphones", "Headphones", 149.99m, 4.4m, 25, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("3917d456-d40a-4518-b3f4-8dcbac6ad9ba"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1683), "Queen size bed frame", "Bed Frame", 299.99m, 4.7m, 5, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("466ea984-fd8b-4875-991a-ebdbef154e9e"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1657), "100% Cotton T-shirt", "T-Shirt", 19.99m, 4.0m, 150, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("7c08b14b-4691-4cc2-82aa-b70f4d4b77f0"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1642), "Fitness tracker", "Smartwatch", 199.99m, 4.1m, 40, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("93a9b88c-28d1-4526-8937-2dd232ba1f1f"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1691), "Soft area rug", "Carpet", 99.99m, 4.3m, 20, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("957f3b51-13e9-427a-b2a4-737fdfb628c8"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1662), "Waterproof winter jacket", "Jacket", 99.99m, 4.3m, 60, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1621), "Latest model smartphone", "Smartphone", 699.99m, 4.5m, 50, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1625), "High-performance laptop", "Laptop", 1299.99m, 4.8m, 20, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("d7d876d1-efb1-4dd4-b866-fba78b91eee4"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1651), "Blue denim jeans", "Jeans", 49.99m, 4.2m, 100, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("e2114ee6-9c66-438e-a002-eff678852bb9"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1687), "LED floor lamp", "Lamp", 49.99m, 4.2m, 30, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("e6efba21-48cf-4430-a0dc-91564b75b8a2"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1671), "Modern 3-seater sofa", "Sofa", 899.99m, 4.6m, 10, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("f5aca46a-7e0f-4d22-96d7-9085fa6d92ab"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1637), "Lightweight and portable", "Tablet", 499.99m, 4.3m, 30, new Guid("44444444-4444-4444-4444-444444444444") }
                });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "BuyerId", "CreatedAt", "ProductId", "SellerId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1733), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new Guid("22222222-2222-2222-2222-222222222222"), null },
                    { new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1734), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new Guid("22222222-2222-2222-2222-222222222222"), null }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "ChatId", "Content", "SenderId", "SentAt" },
                values: new object[,]
                {
                    { new Guid("1cf6997b-fecf-4b45-93e7-6b7a8aa72254"), new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), "It has 16GB RAM and a 1TB SSD.", new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 5, 31, 15, 19, 15, 464, DateTimeKind.Utc).AddTicks(1808) },
                    { new Guid("2b63240b-4bdd-4a65-8b6c-d0a396c22a92"), new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), "Yes, we have 50 units available!", new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 5, 31, 15, 19, 15, 464, DateTimeKind.Utc).AddTicks(1799) },
                    { new Guid("34a73650-7246-4a16-b6f3-51becdb686f0"), new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), "Hi, is the smartphone still in stock?", new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1794) },
                    { new Guid("ba494c11-bb1b-4d8a-a016-2e5f2781b5a2"), new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), "Can you tell me more about the laptop?", new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1806) }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "BuyerId", "ChatId", "CreatedAt", "ProductId", "SellerId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("d2e3f4a5-b6c7-8901-bcde-f23456789012"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1764), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new Guid("22222222-2222-2222-2222-222222222222"), 0, null },
                    { new Guid("f4a5b6c7-d8e9-0123-def0-456789012345"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), new DateTime(2025, 5, 31, 15, 14, 15, 464, DateTimeKind.Utc).AddTicks(1765), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new Guid("22222222-2222-2222-2222-222222222222"), 1, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_BuyerId",
                table: "Chats",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ProductId",
                table: "Chats",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_SellerId",
                table: "Chats",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChatId",
                table: "Messages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_store_id",
                table: "Products",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_UserId",
                table: "Requests",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_BuyerId",
                table: "Sales",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ChatId",
                table: "Sales",
                column: "ChatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductId",
                table: "Sales",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_SellerId",
                table: "Sales",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_UserId",
                table: "Stores",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
