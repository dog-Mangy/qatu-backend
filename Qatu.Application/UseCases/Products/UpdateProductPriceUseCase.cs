using Qatu.Application.DTOs.Product;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Products
{
    public class UpdateProductPriceUseCase
    {
        private readonly IProductRepository _repository;

        public UpdateProductPriceUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(UpdateProductPriceDto command)
        {
            var product = await _repository.GetByIdAsync(command.ProductId);
            if (product == null)
                return false;

            product.Price = command.NewPrice;
            await _repository.UpdateAsync(product);
            return true;
        }
    }
}
