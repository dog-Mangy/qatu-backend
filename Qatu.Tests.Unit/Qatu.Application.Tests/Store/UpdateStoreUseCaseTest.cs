using System;
using System.Threading.Tasks;
using Moq;
using Qatu.Application.DTOs.Store;
using Qatu.Application.UseCases.Stores;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;
using Xunit;

namespace Qatu.Tests.Unit.UseCases.Stores
{
    public class UpdateStoreUseCaseTest
    {
        [Fact]
        public async Task HandleAsync_ShouldUpdateStore_WhenStoreExists()
        {
            // Arrange
            var mockRepo = new Mock<IStoreRepository>();
            var storeId = Guid.NewGuid();
            var store = new Store
            {
                Id = storeId,
                Name = "Old Name",
                Description = "Old Description"
            };
            var dto = new UpdateStoreDto
            {
                Name = "New Name",
                Description = "New Description"
            };

            mockRepo.Setup(r => r.GetByIdAsync(storeId))
                .ReturnsAsync(store);
            mockRepo.Setup(r => r.UpdateAsync(store))
                .Returns(Task.CompletedTask);

            var useCase = new UpdateStoreUseCase(mockRepo.Object);

            // Act
            var result = await useCase.HandleAsync(storeId, dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Description, result.Description);
            mockRepo.Verify(r => r.UpdateAsync(store), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_ShouldReturnNull_WhenStoreDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IStoreRepository>();
            var storeId = Guid.NewGuid();
            var dto = new UpdateStoreDto
            {
                Name = "New Name",
                Description = "New Description"
            };

            mockRepo.Setup(r => r.GetByIdAsync(storeId))
                .ReturnsAsync((Store?)null);

            var useCase = new UpdateStoreUseCase(mockRepo.Object);

            // Act
            var result = await useCase.HandleAsync(storeId, dto);

            // Assert
            Assert.Null(result);
            mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Store>()), Times.Never);
        }
    }
}