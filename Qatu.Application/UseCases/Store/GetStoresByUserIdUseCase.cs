using Qatu.Application.DTOs.Store;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Stores
{
    public class GetStoresByUserIdUseCase
    {
        private readonly IStoreRepository _repository;

        public GetStoresByUserIdUseCase(IStoreRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetStoreDto>> ExecuteAsync(Guid userId)
        {
            var stores = await _repository.GetByUserIdAsync(userId);

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
