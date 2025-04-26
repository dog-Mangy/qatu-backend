using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Products
{
    public class GetProductsByStoreIdUseCase
    {
        private readonly IProductRepository _repository;

        public GetProductsByStoreIdUseCase(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> ExecuteAsync(int storeId)
        {
            return await _repository.GetByStoreIdAsync(storeId);
        }
    }
}
