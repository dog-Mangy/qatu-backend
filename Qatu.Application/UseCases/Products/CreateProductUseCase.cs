using Qatu.Application.DTOs.Product;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Products
{
    public class CreateProductUseCase
    {
        private readonly IProductRepository _repository;

        public CreateProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> HandleAsync(CreateProductDto command)
        {
            Product product = new();

            product.StoreId = command.StoreId;
            product.Name = command.Name;
            product.Description = command.Description;
            product.CategoryId = command.CategoryId;
            product.Price = command.Price;
            product.Stock = command.Stock ?? 0;

            await _repository.AddAsync(product);

            return product;
        }
    }
}
