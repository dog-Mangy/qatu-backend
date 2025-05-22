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
    public class UpdateProductStockUseCaseTest
    {
        [Fact]
        public async Task ExecuteAsync_ShouldUpdateStock_WhenProductExists()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Stock = 5 };
            var dto = new UpdateProductStockDto { ProductId = productId, NewStock = 20 };

            mockRepo.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync(product);
            mockRepo.Setup(r => r.UpdateAsync(product))
                .Returns(Task.CompletedTask);

            var useCase = new UpdateProductStockUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(dto);

            // Assert
            Assert.True(result);
            Assert.Equal(dto.NewStock, product.Stock);
            mockRepo.Verify(r => r.UpdateAsync(product), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var productId = Guid.NewGuid();
            var dto = new UpdateProductStockDto { ProductId = productId, NewStock = 20 };

            mockRepo.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            var useCase = new UpdateProductStockUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(dto);

            // Assert
            Assert.False(result);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Never);
        }
    }
}