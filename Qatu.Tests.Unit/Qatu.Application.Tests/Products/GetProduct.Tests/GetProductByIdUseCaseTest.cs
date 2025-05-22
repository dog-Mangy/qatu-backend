using System;
using System.Threading.Tasks;
using Moq;
using Qatu.Application.UseCases.Products;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;
using Xunit;

namespace Qatu.Tests.Unit.UseCases.Products
{
    public class GetProductByIdUseCaseTest
    {
        [Fact]
        public async Task ExecuteAsync_ShouldReturnProduct_WhenProductExists()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var productId = Guid.NewGuid();
            var expectedProduct = new Product
            {
                Id = productId,
                Name = "Test Product"
            };
            mockRepo.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync(expectedProduct);

            var useCase = new GetProductByIdUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(productId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedProduct.Id, result.Id);
            Assert.Equal(expectedProduct.Name, result.Name);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnNull_WhenProductDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var productId = Guid.NewGuid();
            mockRepo.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            var useCase = new GetProductByIdUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(productId);

            // Assert
            Assert.Null(result);
        }
    }
}