using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Stores
{
    public class GetStoreByIdUseCase
    {
        private readonly IStoreRepository _repository;

        public GetStoreByIdUseCase(IStoreRepository repository)
        {
            _repository = repository;
        }

        public async Task<Store?> ExecuteAsync(Guid storeId)
        {
            return await _repository.GetByIdAsync(storeId);
        }
    }
}
