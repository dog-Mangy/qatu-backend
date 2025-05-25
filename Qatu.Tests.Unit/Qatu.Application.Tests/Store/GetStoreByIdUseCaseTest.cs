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
    public class GetStoreByIdUseCaseTest
    {
        [Fact]
        public async Task ExecuteAsync_ShouldReturnStore_WhenStoreExists()
        {
            // Arrange
            var mockRepo = new Mock<IStoreRepository>();
            var storeId = Guid.NewGuid();
            var store = new Store
            {
                Id = storeId,
                UserId = Guid.NewGuid(),
                Name = "Test Store",
                Description = "Test Description",
                CreatedAt = DateTime.UtcNow
            };

            mockRepo.Setup(r => r.GetByIdAsync(storeId))
                .ReturnsAsync(store);

            var useCase = new GetStoreByIdUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(storeId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(store.Id, result.Id);
            Assert.Equal(store.UserId, result.UserId);
            Assert.Equal(store.Name, result.Name);
            Assert.Equal(store.Description, result.Description);
            Assert.Equal(store.CreatedAt, result.CreatedAt);
        }

        [Fact]
        public async Task ExecuteAsync_ShouldReturnNull_WhenStoreDoesNotExist()
        {
            // Arrange
            var mockRepo = new Mock<IStoreRepository>();
            var storeId = Guid.NewGuid();

            mockRepo.Setup(r => r.GetByIdAsync(storeId))
                .ReturnsAsync((Store?)null);

            var useCase = new GetStoreByIdUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(storeId);

            // Assert
            Assert.Null(result);
        }
    }
}