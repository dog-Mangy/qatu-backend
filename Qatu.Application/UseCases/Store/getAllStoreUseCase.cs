using Qatu.Application.DTOs.Store;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Stores
{
    public class GetStoresUseCase
    {
        private readonly IStoreRepository _repository;

        public GetStoresUseCase(IStoreRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetStoreDto>> ExecuteAsync()
        {
            var stores = await _repository.GetAllAsync();

            return stores.Select(store => new GetStoreDto
            {
                Id = store.Id,
                UserId = store.UserId,
                Name = store.Name,
                Description = store.Description,
                CreatedAt = store.CreatedAt
            });
        }
    }
}
