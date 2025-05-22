using System;
using System.Threading.Tasks;
using Moq;
using Qatu.Application.UseCases.Products;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;
using Xunit;

namespace Qatu.Tests.Unit.UseCases.Products
{
    public class DeleteProductUseCaseTest
    {
        [Fact]
        public async Task ExecuteAsync_ShouldReturnTrue_WhenProductExists()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var productId = Guid.NewGuid();
            mockRepo.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync(new Product { Id = productId });
            mockRepo.Setup(r => r.DeleteAsync(productId))
                .Returns(Task.CompletedTask);

            var useCase = new DeleteProductUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(productId);

            // Assert
            Assert.True(result);
            mockRepo.Verify(r => r.DeleteAsync(productId), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFalse_WhenProductDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var productId = Guid.NewGuid();
            mockRepo.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync((Product?)null);

            var useCase = new DeleteProductUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(productId);

            // Assert
            Assert.False(result);
            mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}