using System;
using System.Threading.Tasks;
using Moq;
using Qatu.Application.UseCases.Stores;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;
using Xunit;

namespace Qatu.Tests.Unit.UseCases.Stores
{
    public class DeleteStoreUseCaseTest
    {
        [Fact]
        public async Task ExecuteAsync_ShouldReturnTrue_WhenStoreExists()
        {
            // Arrange
            var mockRepo = new Mock<IStoreRepository>();
            var storeId = Guid.NewGuid();
            mockRepo.Setup(r => r.GetByIdAsync(storeId))
                .ReturnsAsync(new Store { Id = storeId });
            mockRepo.Setup(r => r.DeleteAsync(storeId))
                .Returns(Task.CompletedTask);

            var useCase = new DeleteStoreUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(storeId);

            // Assert
            Assert.True(result);
            mockRepo.Verify(r => r.DeleteAsync(storeId), Times.Once);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnFalse_WhenStoreDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IStoreRepository>();
            var storeId = Guid.NewGuid();
            mockRepo.Setup(r => r.GetByIdAsync(storeId))
                .ReturnsAsync((Store?)null);

            var useCase = new DeleteStoreUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(storeId);

            // Assert
            Assert.False(result);
            mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Never);
        }
    }
}