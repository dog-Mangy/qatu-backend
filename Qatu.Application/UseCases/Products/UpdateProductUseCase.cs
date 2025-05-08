using Qatu.Domain.Interfaces;
using Qatu.Application.DTOs.Product;

namespace Qatu.Application.UseCases.Products
{
    public class UpdateProductUseCase
    {
        private readonly IProductRepository _repository;

        public UpdateProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(UpdateProductDto command)
        {
            var product = await _repository.GetByIdAsync(command.ProductId);
            if (product == null)
                return false;

            product.StoreId = command.StoreId;
            product.Name = command.Name;
            product.Description = command.Description;
            product.Category = command.Category;
            product.Price = command.Price;
            product.Rating = command.Rating;
            product.Stock = command.Stock;

            await _repository.UpdateAsync(product);
            return true;
        }
    }
}
