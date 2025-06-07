using System;
using System.Threading.Tasks;
using Moq;
using Qatu.Application.DTOs.Product;
using Qatu.Application.UseCases.Products;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;
using Xunit;

namespace Qatu.Tests.Unit.UseCases.Products
{
    public class CreateProductUseCaseTest
    {
        [Fact]
        public async Task HandleAsync_ShouldCreateProductAndReturnIt()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var dto = new CreateProductDto
            {
                StoreId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(), // ← Corregido aquí
                Name = "Test Product",
                Description = "Test Description",
                Price = 100,
                Stock = 5
            };

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            var useCase = new CreateProductUseCase(mockRepo.Object);

            // Act
            var result = await useCase.HandleAsync(dto);

            // Assert
            mockRepo.Verify(r => r.AddAsync(It.Is<Product>(p =>
                p.StoreId == dto.StoreId &&
                p.CategoryId == dto.CategoryId && // ← Corregido aquí
                p.Name == dto.Name &&
                p.Description == dto.Description &&
                p.Price == dto.Price &&
                p.Stock == dto.Stock
            )), Times.Once);

            Assert.Equal(dto.CategoryId, result.CategoryId); // ← Corregido aquí
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Description, result.Description);
            Assert.Equal(dto.Price, result.Price);
            Assert.Equal(dto.Stock, result.Stock);
        }

        [Fact]
        public async Task HandleAsync_ShouldSetStockToZero_WhenStockIsNull()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var dto = new CreateProductDto
            {
                StoreId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(), // ← Corregido aquí
                Name = "Test Product",
                Description = "Test Description",
                Price = 100,
                Stock = null
            };

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Product>()))
                .Returns(Task.CompletedTask);

            var useCase = new CreateProductUseCase(mockRepo.Object);

            // Act
            var result = await useCase.HandleAsync(dto);

            // Assert
            Assert.Equal(0, result.Stock);
        }
    }
}
