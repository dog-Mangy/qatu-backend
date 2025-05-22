using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Qatu.Application.DTOs;
using Qatu.Application.DTOs.Product;
using Qatu.Application.UseCases.Products;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;
using Xunit;

namespace Qatu.Tests.Unit.UseCases.Products
{
    public class GetProductsUseCaseTest
    {
        
        [Fact]
        public async Task ExecuteAsync_WithStoreId_CallsRepositoryWithStoreId()
        {
            // Arrange
            var mockRepo = new Mock<IProductRepository>();
            var storeId = Guid.NewGuid();
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Prod1", StoreId = storeId }
            };
            mockRepo.Setup(r => r.CountAsync(storeId))
                .ReturnsAsync(products.Count);
            mockRepo.Setup(r => r.GetPagedFilteredAndSortedAsync(
                null, null, null, null, null, null, null, true, 1, 10, storeId))
                .ReturnsAsync(products);

            var useCase = new GetProductsUseCase(mockRepo.Object);

            // Act
            var result = await useCase.ExecuteAsync(1, 10, storeId: storeId);

            // Assert
            Assert.NotNull(result.Items);
            var items = result.Items.ToList();
            Assert.Single(items);
            Assert.Equal(storeId, items[0].StoreId);
        }
    }
}