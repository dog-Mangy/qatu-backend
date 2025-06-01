using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qatu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateV5 : Migration
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
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(8951), null, null, "Electronics", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(8953), null, null, "Clothing", null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(8955), null, null, "Furniture", null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null, new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(8959), null, null, "Home Decor", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Role" },
                values: new object[,]
                {
                    { new Guid("44147d07-ae1c-45ae-af1c-984ff4d49eba"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(8757), "admin@qatu.com", "Admin User", 0 },
                    { new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(8774), "buyer@qatu.com", "Buyer User", 1 },
                    { new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(8772), "seller@qatu.com", "Seller User", 2 }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "CreatedAt", "Description", "Status", "StoreDescription", "StoreName", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(9937), "Admin verification request", 0, "Store managed by admin", "Admin Store", null, new Guid("44147d07-ae1c-45ae-af1c-984ff4d49eba") },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(9940), "Request to update store info", 0, "Electronics and gadgets", "SuperElectro", null, new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869") },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(9942), "Support needed for order issue", 0, "Store created for buyer support request", "Temporary Buyer Store", null, new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74") }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(8921), "Electronics and gadgets", "Tech Store", new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(8923), "Clothing and accessories", "Fashion Store", new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(8925), "Home essentials and furniture", "Home Store", new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "created_at", "Description", "Name", "Price", "Rating", "Stock", "store_id" },
                values: new object[,]
                {
                    { new Guid("119886a5-c965-43ff-bcf8-cb4c95135066"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(9976), "Lightweight and portable", "Tablet", 499.99m, 4.3m, 30, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("30a8c927-9774-4057-9f6a-2c4158d2b2d3"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(9987), "Noise-canceling headphones", "Headphones", 149.99m, 4.4m, 25, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("377f3896-ed4d-4e68-ad30-02cc65e426b8"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(9990), "Blue denim jeans", "Jeans", 49.99m, 4.2m, 100, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("39ebf0fb-2b2e-49ac-8c7c-056cc5f1d4cd"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 6, 1, 23, 1, 5, 488, DateTimeKind.Utc).AddTicks(9), "Queen size bed frame", "Bed Frame", 299.99m, 4.7m, 5, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("45726569-0a33-4ed1-9ce3-072324033594"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(9993), "100% Cotton T-shirt", "T-Shirt", 19.99m, 4.0m, 150, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("493d166f-58c8-4c02-ac07-efff021bdb13"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 6, 1, 23, 1, 5, 488, DateTimeKind.Utc).AddTicks(6), "Solid wood dining table", "Dining Table", 499.99m, 4.5m, 15, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("73e0fe7b-530b-4cc1-9ad0-a028f116b0a8"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(9996), "Waterproof winter jacket", "Jacket", 99.99m, 4.3m, 60, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("7a44753c-aa8c-480f-ade8-bcf20ab8a6ce"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(9999), "Comfortable sneakers", "Sneakers", 59.99m, 4.2m, 120, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("81461fab-579e-4b88-8406-8c66890f59a3"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 6, 1, 23, 1, 5, 488, DateTimeKind.Utc).AddTicks(1), "Modern 3-seater sofa", "Sofa", 899.99m, 4.6m, 10, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("83386c63-5b7e-45bc-8720-6e9c3185ede5"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 6, 1, 23, 1, 5, 488, DateTimeKind.Utc).AddTicks(12), "LED floor lamp", "Lamp", 49.99m, 4.2m, 30, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(9969), "Latest model smartphone", "Smartphone", 699.99m, 4.5m, 50, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(9972), "High-performance laptop", "Laptop", 1299.99m, 4.8m, 20, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("b3daaaef-3854-4570-ac28-4b7daa08b74f"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 6, 1, 23, 1, 5, 487, DateTimeKind.Utc).AddTicks(9982), "Fitness tracker", "Smartwatch", 199.99m, 4.1m, 40, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("bb267be4-5ccd-4728-9ee5-422a2bd10c07"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 6, 1, 23, 1, 5, 488, DateTimeKind.Utc).AddTicks(14), "Soft area rug", "Carpet", 99.99m, 4.3m, 20, new Guid("66666666-6666-6666-6666-666666666666") }
                });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "BuyerId", "CreatedAt", "ProductId", "SellerId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74"), new DateTime(2025, 6, 1, 23, 1, 5, 488, DateTimeKind.Utc).AddTicks(41), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869"), null },
                    { new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74"), new DateTime(2025, 6, 1, 23, 1, 5, 488, DateTimeKind.Utc).AddTicks(42), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869"), null }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "ChatId", "Content", "SenderId", "SentAt" },
                values: new object[,]
                {
                    { new Guid("1819bd1a-7ca1-41f7-b730-2db6f90c840d"), new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), "Yes, we have 50 units available!", new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869"), new DateTime(2025, 6, 1, 23, 6, 5, 488, DateTimeKind.Utc).AddTicks(79) },
                    { new Guid("3166aa4c-3482-4a86-80b6-5da56cd05a9b"), new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), "It has 16GB RAM and a 1TB SSD.", new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869"), new DateTime(2025, 6, 1, 23, 6, 5, 488, DateTimeKind.Utc).AddTicks(83) },
                    { new Guid("32bc6551-6497-4e0c-a36a-e7cce38e5f54"), new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), "Hi, is the smartphone still in stock?", new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74"), new DateTime(2025, 6, 1, 23, 1, 5, 488, DateTimeKind.Utc).AddTicks(77) },
                    { new Guid("66f642f3-1af6-4396-888c-19b49b8ced89"), new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), "Can you tell me more about the laptop?", new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74"), new DateTime(2025, 6, 1, 23, 1, 5, 488, DateTimeKind.Utc).AddTicks(82) }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "BuyerId", "ChatId", "CreatedAt", "ProductId", "SellerId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("d2e3f4a5-b6c7-8901-bcde-f23456789012"), new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74"), new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), new DateTime(2025, 6, 1, 23, 1, 5, 488, DateTimeKind.Utc).AddTicks(59), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869"), 0, null },
                    { new Guid("f4a5b6c7-d8e9-0123-def0-456789012345"), new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74"), new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), new DateTime(2025, 6, 1, 23, 1, 5, 488, DateTimeKind.Utc).AddTicks(59), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869"), 1, null }
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
