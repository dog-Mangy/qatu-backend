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
    public class UpdateProductUseCaseTest
    {
        [Fact]
        public async Task ExecuteAsync_ShouldUpdateProduct_WhenProductExists()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId };
            var dto = new UpdateProductDto
            {
                StoreId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(), // ← Corregido
                Name = "Nuevo nombre",
                Description = "Nueva descripción",
                Price = 123.45m,
                Rating = 4.5m,
                Stock = 10
            };

            mockRepo.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync(product);
            mockRepo.Setup(r => r.UpdateAsync(product))
                .Returns(Task.CompletedTask);

            var useCase = new UpdateProductUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(productId, dto);

            // Assert
            Assert.True(result);
            Assert.Equal(dto.StoreId, product.StoreId);
            Assert.Equal(dto.CategoryId, product.CategoryId); // ← Corregido
            Assert.Equal(dto.Name, product.Name);
            Assert.Equal(dto.Description, product.Description);
            Assert.Equal(dto.Price, product.Price);
            Assert.Equal(dto.Rating, product.Rating);
            Assert.Equal(dto.Stock, product.Stock);
            mockRepo.Verify(r => r.UpdateAsync(product), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var productId = Guid.NewGuid();
            var dto = new UpdateProductDto
            {
                StoreId = Guid.NewGuid(),
                CategoryId = Guid.NewGuid(), // ← Corregido
                Name = "Nuevo nombre",
                Description = "Nueva descripción",
                Price = 123.45m,
                Rating = 4.5m,
                Stock = 10
            };

            mockRepo.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            var useCase = new UpdateProductUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(productId, dto);

            // Assert
            Assert.False(result);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }
    }
}
