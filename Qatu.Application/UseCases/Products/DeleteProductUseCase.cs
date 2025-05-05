using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Products
{
    public class DeleteProductUseCase
    {
        private readonly IProductRepository _repository;

        public DeleteProductUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(Guid id)
        {
            var existingProduct = await _repository.GetByIdAsync(id);
            if (existingProduct == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
