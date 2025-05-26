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
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7305), null, null, "Electronics", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7311), null, null, "Clothing", null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7317), null, null, "Furniture", null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null, new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7323), null, null, "Home Decor", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7081), "admin@qatu.com", "Admin User", "password123", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7091), "seller@qatu.com", "Seller User", "password123", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7097), "buyer@qatu.com", "Buyer User", "password123", 1 }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7267), "Electronics and gadgets", "Tech Store", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7274), "Clothing and accessories", "Fashion Store", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7280), "Home essentials and furniture", "Home Store", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "created_at", "Description", "Name", "Price", "Rating", "Stock", "store_id" },
                values: new object[,]
                {
                    { new Guid("2866f0f9-e58a-47e3-903c-ee1e15e511b5"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7496), "Soft area rug", "Carpet", 99.99m, 4.3m, 20, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("288b0bd5-a11f-4ff8-81b9-ac7f3f2dcc3b"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7440), "Comfortable sneakers", "Sneakers", 59.99m, 4.2m, 120, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("30a8c927-9774-4057-9f6a-2c4158d2b2d3"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7451), "Modern 3-seater sofa", "Sofa", 899.99m, 4.6m, 10, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("377f3896-ed4d-4e68-ad30-02cc65e426b8"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7485), "LED floor lamp", "Lamp", 49.99m, 4.2m, 30, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("45726569-0a33-4ed1-9ce3-072324033594"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7419), "100% Cotton T-shirt", "T-Shirt", 19.99m, 4.0m, 150, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("73e0fe7b-530b-4cc1-9ad0-a028f116b0a8"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7385), "Fitness tracker", "Smartwatch", 199.99m, 4.1m, 40, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("7a44753c-aa8c-480f-ade8-bcf20ab8a6ce"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7462), "Solid wood dining table", "Dining Table", 499.99m, 4.5m, 15, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("81461fab-579e-4b88-8406-8c66890f59a3"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7396), "Noise-canceling headphones", "Headphones", 149.99m, 4.4m, 25, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("854388f2-1520-44c4-a7d6-998f873e8eb2"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7374), "Lightweight and portable", "Tablet", 499.99m, 4.3m, 30, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("91e729f1-d4ce-4c0d-9ee8-d1e213cfd061"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7407), "Blue denim jeans", "Jeans", 49.99m, 4.2m, 100, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7349), "Latest model smartphone", "Smartphone", 699.99m, 4.5m, 50, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7362), "High-performance laptop", "Laptop", 1299.99m, 4.8m, 20, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("c1193256-c9b7-4223-85df-eef8ed651ae7"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7430), "Waterproof winter jacket", "Jacket", 99.99m, 4.3m, 60, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("c61e95c0-d2d4-4d56-81bc-c448810e3d2b"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7474), "Queen size bed frame", "Bed Frame", 299.99m, 4.7m, 5, new Guid("66666666-6666-6666-6666-666666666666") }
                });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "BuyerId", "CreatedAt", "ProductId", "SellerId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7522), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new Guid("22222222-2222-2222-2222-222222222222"), null },
                    { new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7523), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new Guid("22222222-2222-2222-2222-222222222222"), null }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "ChatId", "Content", "SenderId", "SentAt" },
                values: new object[,]
                {
                    { new Guid("23e2dfa1-94c9-4738-9f4f-21490837a041"), new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), "Hi, is the smartphone still in stock?", new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7570) },
                    { new Guid("525758af-0987-4080-9aa1-2284f98ff67a"), new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), "Yes, we have 50 units available!", new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 5, 26, 0, 32, 1, 17, DateTimeKind.Utc).AddTicks(7576) },
                    { new Guid("7f8721c3-74ab-44f9-9d59-628cce638251"), new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), "It has 16GB RAM and a 1TB SSD.", new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 5, 26, 0, 32, 1, 17, DateTimeKind.Utc).AddTicks(7592) },
                    { new Guid("b2104b96-3821-4f1a-aa37-4ae6d82ee72e"), new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), "Can you tell me more about the laptop?", new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7586) }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "BuyerId", "ChatId", "CreatedAt", "ProductId", "SellerId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("d2e3f4a5-b6c7-8901-bcde-f23456789012"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7546), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new Guid("22222222-2222-2222-2222-222222222222"), 0, null },
                    { new Guid("f4a5b6c7-d8e9-0123-def0-456789012345"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), new DateTime(2025, 5, 26, 0, 27, 1, 17, DateTimeKind.Utc).AddTicks(7547), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new Guid("22222222-2222-2222-2222-222222222222"), 1, null }
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
