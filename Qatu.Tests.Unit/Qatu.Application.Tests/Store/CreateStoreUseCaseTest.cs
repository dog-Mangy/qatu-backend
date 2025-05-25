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
    public class CreateStoreUseCaseTest
    {
        [Fact]
        public async Task HandleAsync_ShouldCreateStoreAndReturnIt()
        {
            // Arrange
            var mockRepo = new Mock<IStoreRepository>();
            var dto = new CreateStoreDto
            {
                UserId = Guid.NewGuid(),
                Name = "Tienda de Prueba",
                Description = "Descripción de prueba"
            };

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Store>()))
                .Returns(Task.CompletedTask);

            var useCase = new CreateStoreUseCase(mockRepo.Object);

            // Act
            var result = await useCase.HandleAsync(dto);

            // Assert
            Assert.Equal(dto.UserId, result.UserId);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Description, result.Description);
            mockRepo.Verify(r => r.AddAsync(It.Is<Store>(s =>
                s.UserId == dto.UserId &&
                s.Name == dto.Name &&
                s.Description == dto.Description
            )), Times.Once);
        }
    }
}