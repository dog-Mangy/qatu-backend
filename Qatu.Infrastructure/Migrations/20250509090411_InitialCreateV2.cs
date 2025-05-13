using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qatu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
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
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "longtext", nullable: false)
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
                        name: "FK_Products_Stores_store_id",
                        column: x => x.store_id,
                        principalTable: "Stores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3756), "admin@qatu.com", "Admin User", "password123", 0 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3758), "seller@qatu.com", "Seller User", "password123", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3777), "buyer@qatu.com", "Buyer User", "password123", 1 }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3898), "Electronics and gadgets", "Tech Store", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3900), "Clothing and accessories", "Fashion Store", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3902), "Home essentials and furniture", "Home Store", new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "created_at", "Description", "Name", "Price", "Rating", "Stock", "store_id" },
                values: new object[,]
                {
                    { new Guid("0a262832-a2ca-4540-ae62-4ee9f458c8e9"), "Electronics", new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3941), "Lightweight and portable", "Tablet", 499.99m, 4.3m, 30, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("1ba43fc4-44ea-40d6-83b6-df9bdec60137"), "Footwear", new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3958), "Comfortable sneakers", "Sneakers", 59.99m, 4.2m, 120, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("46af3683-63e8-4834-8af4-0d95d118dbff"), "Furniture", new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3961), "Modern 3-seater sofa", "Sofa", 899.99m, 4.6m, 10, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("6883ab22-dca9-4f24-b96a-7a926f0a8cf5"), "Electronics", new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3936), "High-performance laptop", "Laptop", 1299.99m, 4.8m, 20, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("7066cd87-5f16-4def-ab73-a08db351afda"), "Home Decor", new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3973), "Soft area rug", "Carpet", 99.99m, 4.3m, 20, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("7a596c9e-2ae1-4d62-9d44-aecebcf36c80"), "Furniture", new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3968), "Queen size bed frame", "Bed Frame", 299.99m, 4.7m, 5, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("7feb748a-1e09-4f40-a0a9-cbcd9d69bc65"), "Clothing", new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3956), "Waterproof winter jacket", "Jacket", 99.99m, 4.3m, 60, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("7ff0c73f-d54a-451f-ab91-5ef88b6d931a"), "Home Decor", new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3970), "LED floor lamp", "Lamp", 49.99m, 4.2m, 30, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("d9112846-d97d-4395-8e37-f855759aa650"), "Electronics", new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3927), "Latest model smartphone", "Smartphone", 699.99m, 4.5m, 50, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("daa9f12e-1db2-4a53-a159-3f520bb6eafb"), "Wearables", new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3944), "Fitness tracker", "Smartwatch", 199.99m, 4.1m, 40, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("dafd71a8-2a0c-4816-bfad-dc08a0acba16"), "Furniture", new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3965), "Solid wood dining table", "Dining Table", 499.99m, 4.5m, 15, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("e09bb8f2-834f-4572-a3d4-596d66930f62"), "Accessories", new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3946), "Noise-canceling headphones", "Headphones", 149.99m, 4.4m, 25, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("eb560c7a-2d62-443d-97b5-117ae42c73f0"), "Clothing", new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3953), "100% Cotton T-shirt", "T-Shirt", 19.99m, 4.0m, 150, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("eb8183f3-3193-43f4-a5a6-6ee4b7dd9588"), "Clothing", new DateTime(2025, 5, 9, 9, 4, 10, 850, DateTimeKind.Utc).AddTicks(3949), "Blue denim jeans", "Jeans", 49.99m, 4.2m, 100, new Guid("55555555-5555-5555-5555-555555555555") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_store_id",
                table: "Products",
                column: "store_id");

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
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
