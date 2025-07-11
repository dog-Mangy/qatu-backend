﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qatu.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatev6 : Migration
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
                    Image = table.Column<string>(type: "longtext", nullable: false)
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
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(4232), null, null, "Electronics", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(4239), null, null, "Clothing", null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(4244), null, null, "Furniture", null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null, new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(4250), null, null, "Home Decor", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Role" },
                values: new object[,]
                {
                    { new Guid("44147d07-ae1c-45ae-af1c-984ff4d49eba"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(4045), "admin@qatu.com", "Admin User", 0 },
                    { new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(4059), "buyer@qatu.com", "Buyer User", 1 },
                    { new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(4053), "seller@qatu.com", "Seller User", 2 }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "CreatedAt", "Description", "Status", "StoreDescription", "StoreName", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5434), "Admin verification request", 0, "Store managed by admin", "Admin Store", null, new Guid("44147d07-ae1c-45ae-af1c-984ff4d49eba") },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5441), "Request to update store info", 0, "Electronics and gadgets", "SuperElectro", null, new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869") },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5447), "Support needed for order issue", 0, "Store created for buyer support request", "Temporary Buyer Store", null, new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74") }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UserId" },
                values: new object[,]
                {
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(4193), "Electronics and gadgets", "Tech Store", new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869") },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(4200), "Clothing and accessories", "Fashion Store", new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869") },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(4205), "Home essentials and furniture", "Home Store", new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869") }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "created_at", "Description", "Image", "Name", "Price", "Rating", "Stock", "store_id" },
                values: new object[,]
                {
                    { new Guid("21e26ed0-51e9-44ec-add9-5143a50438d1"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5603), "Soft area rug", "https://i.ibb.co/SWjnYY1/81-Ry-C8-YEst-L-AC-SL1500.jpg", "Carpet", 99.99m, 4.3m, 20, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("2b8f28bc-e742-4fff-929a-9281547058cc"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5508), "Fitness tracker", "https://i.ibb.co/fGrpbQMJ/smartwatch.jpg", "Smartwatch", 199.99m, 4.1m, 40, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("30a8c927-9774-4057-9f6a-2c4158d2b2d3"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5520), "Noise-canceling headphones", "https://i.ibb.co/4R8qDKD2/hheadphones.jpg", "Headphones", 149.99m, 4.4m, 25, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("377f3896-ed4d-4e68-ad30-02cc65e426b8"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5528), "Blue denim jeans", "https://i.ibb.co/0RZTny2w/jeans.jpg", "Jeans", 49.99m, 4.2m, 100, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("45726569-0a33-4ed1-9ce3-072324033594"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5535), "100% Cotton T-shirt", "https://i.ibb.co/XZCpMr3Z/tshirt.jpg", "T-Shirt", 19.99m, 4.0m, 150, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("580eb52d-ade2-4ace-b63d-d905c97d2bd4"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5576), "Queen size bed frame", "https://i.ibb.co/84xwsfWB/bed.jpg", "Bed Frame", 299.99m, 4.7m, 5, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("6acd6f4a-7970-4b83-83ff-439bdde85bce"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5565), "Solid wood dining table", "https://i.ibb.co/hxhd6GTZ/0403037-dallas-ranch-rustic-solid-wood-double-pedestal-dining-table-set.jpg", "Dining Table", 499.99m, 4.5m, 15, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("73e0fe7b-530b-4cc1-9ad0-a028f116b0a8"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5541), "Waterproof winter jacket", "https://i.ibb.co/9HjpFWPM/jacket.jpg", "Jacket", 99.99m, 4.3m, 60, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("7a44753c-aa8c-480f-ade8-bcf20ab8a6ce"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5548), "Comfortable sneakers", "https://i.ibb.co/k2vTgq3j/sneakers.png", "Sneakers", 59.99m, 4.2m, 120, new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("81461fab-579e-4b88-8406-8c66890f59a3"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5554), "Modern 3-seater sofa", "https://i.ibb.co/ksVP0spL/sofa.jpg", "Sofa", 899.99m, 4.6m, 10, new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("85c69d14-04ab-4edd-a727-193c0aa364e0"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5498), "Lightweight and portable", "https://i.ibb.co/B2BpfVrx/huawei-matepad-11-5-s-grey.jpg", "Tablet", 499.99m, 4.3m, 30, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5479), "Latest model smartphone", "https://i.ibb.co/xtbmB42y/W-O-X500-Global-4-G-LTE-6-5-Inch-Quad-Core-Big-Battery-Play-Store-Support-Android-Smartphone.jpg", "Smartphone", 699.99m, 4.5m, 50, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5487), "High-performance laptop", "https://i.ibb.co/x8BXFDqV/laptop.jpg", "Laptop", 1299.99m, 4.8m, 20, new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("fef516da-eb8e-4798-8d01-17750eeb8a03"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5592), "LED floor lamp", "https://i.ibb.co/bMHCSV6p/led.png", "Lamp", 49.99m, 4.2m, 30, new Guid("66666666-6666-6666-6666-666666666666") }
                });

            migrationBuilder.InsertData(
                table: "Chats",
                columns: new[] { "Id", "BuyerId", "CreatedAt", "ProductId", "SellerId", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5632), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869"), null },
                    { new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5633), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869"), null }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "ChatId", "Content", "SenderId", "SentAt" },
                values: new object[,]
                {
                    { new Guid("2ef25744-0d42-4b66-976f-1b06a5a80fb7"), new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), "It has 16GB RAM and a 1TB SSD.", new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869"), new DateTime(2025, 6, 2, 6, 54, 2, 151, DateTimeKind.Utc).AddTicks(5702) },
                    { new Guid("71a4d29d-38cc-47a8-b6c1-3306f1000b80"), new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), "Hi, is the smartphone still in stock?", new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5680) },
                    { new Guid("b3918eec-2072-43fb-8f30-5a5d019080d3"), new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), "Yes, we have 50 units available!", new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869"), new DateTime(2025, 6, 2, 6, 54, 2, 151, DateTimeKind.Utc).AddTicks(5685) },
                    { new Guid("fe501d62-3ae4-427b-90d4-91e63ce75e32"), new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), "Can you tell me more about the laptop?", new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5697) }
                });

            migrationBuilder.InsertData(
                table: "Sales",
                columns: new[] { "Id", "BuyerId", "ChatId", "CreatedAt", "ProductId", "SellerId", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("d2e3f4a5-b6c7-8901-bcde-f23456789012"), new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74"), new Guid("c1d2e3f4-a5b6-7890-abcd-ef1234567890"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5653), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869"), 0, null },
                    { new Guid("f4a5b6c7-d8e9-0123-def0-456789012345"), new Guid("87b84069-9bda-4b44-8d0d-d52c3a8bbc74"), new Guid("e3f4a5b6-c7d8-9012-cdef-345678901234"), new DateTime(2025, 6, 2, 6, 49, 2, 151, DateTimeKind.Utc).AddTicks(5655), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new Guid("a89bff1d-2db6-4e9e-8fd2-bf3ee3ed8869"), 1, null }
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
