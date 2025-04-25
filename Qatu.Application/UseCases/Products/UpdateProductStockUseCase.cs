using Qatu.Domain.Interfaces;
using Qatu.Application.DTOs;

namespace Qatu.Application.UseCases.Products
{
    public class UpdateProductStockUseCase
    {
        private readonly IProductRepository _repository;

        public UpdateProductStockUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(UpdateProductStockDto command)
        {
            var product = await _repository.GetByIdAsync(command.ProductId);
            if (product == null)
                return false;

            product.Stock = command.NewStock;
            await _repository.UpdateAsync(product);
            return true;
        }
    }
}
