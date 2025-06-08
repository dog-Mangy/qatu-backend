// File: backend/Qatu.Tests.Unit/UseCases/Products/CreateProductListUseCaseTest.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Qatu.Application.DTOs.Product;
using Qatu.Application.UseCases.Products;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;
using Xunit;

namespace Qatu.Tests.Unit.UseCases.Products
{
    public class CreateProductListUseCaseTest
    {
        [Fact]
        public async Task HandleAsync_ShouldCreateProductsAndReturnThem()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var dtos = new List<CreateProductDto>
            {
                new CreateProductDto
                {
                    StoreId = Guid.NewGuid(),
                    CategoryId = Guid.NewGuid(),
                    Name = "Prod1",
                    Description = "Desc1",
                    Price = 10,
                    Stock = 5
                },
                new CreateProductDto
                {
                    StoreId = Guid.NewGuid(),
                    CategoryId = Guid.NewGuid(),
                    Name = "Prod2",
                    Description = "Desc2",
                    Price = 20,
                    Stock = 7
                }
            };

            var expectedProducts = dtos.Select(dto => new Product
            {
                StoreId = dto.StoreId,
                CategoryId = dto.CategoryId,
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Stock = dto.Stock ?? 0
            }).ToList();

            mockRepo.Setup(r => r.AddAsyncRange(It.IsAny<List<Product>>()))
                .ReturnsAsync((List<Product> products) => products);

            var useCase = new CreateProductListUseCase(mockRepo.Object);

            // Act
            var result = (await useCase.HandleAsync(dtos)).ToList();

            // Assert
            mockRepo.Verify(r => r.AddAsyncRange(It.Is<List<Product>>(p =>
                p.Count == dtos.Count &&
                p[0].Name == dtos[0].Name &&
                p[1].Name == dtos[1].Name
            )), Times.Once);

            Assert.Equal(expectedProducts.Count, result.Count);
            Assert.Equal(expectedProducts[0].Name, result[0].Name);
            Assert.Equal(expectedProducts[1].Name, result[1].Name);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnEmptyList_WhenInputIsEmpty()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var dtos = new List<CreateProductDto>();

            mockRepo.Setup(r => r.AddAsyncRange(It.IsAny<List<Product>>()))
                .ReturnsAsync(new List<Product>());

            var useCase = new CreateProductListUseCase(mockRepo.Object);

            // Act
            var result = await useCase.HandleAsync(dtos);

            // Assert
            mockRepo.Verify(r => r.AddAsyncRange(It.Is<List<Product>>(p => p.Count == 0)), Times.Once);
            Assert.Empty(result);
        }

        [Fact]
        public async Task HandleAsync_ShouldSetStockToZero_WhenStockIsNull()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var dtos = new List<CreateProductDto>
            {
                new CreateProductDto
                {
                    StoreId = Guid.NewGuid(),
                    CategoryId = Guid.NewGuid(),
                    Name = "Prod",
                    Description = "Desc",
                    Price = 10,
                    Stock = null
                }
            };

            mockRepo.Setup(r => r.AddAsyncRange(It.IsAny<List<Product>>()))
                .ReturnsAsync((List<Product> products) => products);

            var useCase = new CreateProductListUseCase(mockRepo.Object);

            // Act
            var result = (await useCase.HandleAsync(dtos)).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal(0, result[0].Stock);
        }
    }
}
