using Qatu.Application.DTOs.Product;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Products
{
    public class CreateProductListUseCase
    {
        private readonly IProductRepository _repository;

        public CreateProductListUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> HandleAsync(List<CreateProductDto> dtos)
        {
            var products = dtos.Select(dto => new Product
            {
                StoreId = dto.StoreId,
                Name = dto.Name,
                Description = dto.Description,
                Category = dto.Category,
                Price = dto.Price,
                Stock = dto.Stock ?? 0,
            }).ToList();

            return await _repository.AddAsyncRange(products);
        }
    }
}
