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

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryId", "CreatedAt", "Description", "ImageUrl", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 5, 31, 1, 44, 27, 306, DateTimeKind.Utc).AddTicks(7785), null, null, "Electronics", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 5, 31, 1, 44, 27, 306, DateTimeKind.Utc).AddTicks(7788), null, null, "Clothing", null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 5, 31, 1, 44, 27, 306, DateTimeKind.Utc).AddTicks(7790), null, null, "Furniture", null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null, new DateTime(2025, 5, 31, 1, 44, 27, 306, DateTimeKind.Utc).AddTicks(7792), null, null, "Home Decor", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 5, 31, 1, 44, 27, 306, DateTimeKind.Utc).AddTicks(7530), "admin@qatu.com", "Admin User", "password123", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 5, 31, 1, 44, 27, 306, DateTimeKind.Utc).AddTicks(7537), "seller@qatu.com", "Seller User", "password123", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 5, 31, 1, 44, 27, 306, DateTimeKind.Utc).AddTicks(7539), "buyer@qatu.com", "Buyer User", "password123", 1 }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "CreatedAt", "Description", "Status", "StoreDescription", "StoreName", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(379), "Admin verification request", 0, "Store managed by admin", "Admin Store", null, new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(383), "Request to update store info", 0, "Electronics and gadgets", "SuperElectro", null, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(386), "Support needed for order issue", 0, "Store created for buyer support request", "Temporary Buyer Store", null, new Guid("33333333-3333-3333-3333-333333333333") }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 5, 31, 1, 44, 27, 306, DateTimeKind.Utc).AddTicks(7724), "Electronics and gadgets", "Tech Store", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 5, 31, 1, 44, 27, 306, DateTimeKind.Utc).AddTicks(7727), "Clothing and accessories", "Fashion Store", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 5, 31, 1, 44, 27, 306, DateTimeKind.Utc).AddTicks(7754), "Home essentials and furniture", "Home Store", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "created_at", "Description", "Name", "Price", "Rating", "Stock", "store_id" },
                values: new object[,]
                {
                    { new Guid("014d9f9a-a0c0-42d2-a0b3-a7ca3a25a824"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(443), "High-performance laptop", "Laptop", 1299.99m, 4.8m, 20, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("0c76cf62-2d6d-40ae-a829-78d0ef7d1f5b"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(491), "Queen size bed frame", "Bed Frame", 299.99m, 4.7m, 5, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("0f2f4f54-9318-45dd-8550-efb5a0dcc591"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(501), "Soft area rug", "Carpet", 99.99m, 4.3m, 20, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("147e5441-7566-4f91-abea-d321e6faa92a"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(438), "Latest model smartphone", "Smartphone", 699.99m, 4.5m, 50, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("2ab29677-42e9-45c6-ad0c-2b8aa2d730c4"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(452), "Fitness tracker", "Smartwatch", 199.99m, 4.1m, 40, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("2bdbcfba-13d0-471b-bdfa-2bc260d219d5"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(472), "Waterproof winter jacket", "Jacket", 99.99m, 4.3m, 60, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("40f1f928-62c5-43c3-a2a7-0e1158330346"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(447), "Lightweight and portable", "Tablet", 499.99m, 4.3m, 30, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("829c0820-9365-4629-ab39-e627eec0b794"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(463), "Blue denim jeans", "Jeans", 49.99m, 4.2m, 100, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("a03f5b86-abb8-4e33-b4f9-f782601f6fff"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(479), "Comfortable sneakers", "Sneakers", 59.99m, 4.2m, 120, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("a5776663-ee7e-4e7f-bfa6-58a89732aed8"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(468), "100% Cotton T-shirt", "T-Shirt", 19.99m, 4.0m, 150, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("cbbb1ecf-cd14-4c20-bce4-b65ec3a67114"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(487), "Solid wood dining table", "Dining Table", 499.99m, 4.5m, 15, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("cdadfe30-7cc1-49dc-aae0-83cc85dd432a"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(497), "LED floor lamp", "Lamp", 49.99m, 4.2m, 30, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("ce2cfa98-6adb-4e90-9bc8-54c7ad3b3026"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(483), "Modern 3-seater sofa", "Sofa", 899.99m, 4.6m, 10, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("f0b08ca7-017e-4e79-90b5-4c4d0c145d12"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 5, 31, 1, 44, 27, 307, DateTimeKind.Utc).AddTicks(459), "Noise-canceling headphones", "Headphones", 149.99m, 4.4m, 25, new Guid("44444444-4444-4444-4444-444444444444") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CategoryId",
                table: "Categories",
                column: "CategoryId");

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
                name: "IX_Stores_UserId",
                table: "Stores",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
