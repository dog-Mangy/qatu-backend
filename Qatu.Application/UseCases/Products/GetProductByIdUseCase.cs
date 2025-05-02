using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Products
{
    public class GetProductByIdUseCase
    {
        private readonly IProductRepository _repository;

        public GetProductByIdUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product?> ExecuteAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}
