using Qatu.Application.DTOs.Store;
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

        public async Task<GetStoreDto?> ExecuteAsync(Guid storeId)
        {
            var store = await _repository.GetByIdAsync(storeId);

            if (store == null)
                return null;

            return new GetStoreDto
            {
                Id = store.Id,
                UserId = store.UserId,
                Name = store.Name,
                Description = store.Description,
                CreatedAt = store.CreatedAt
            };
        }
    }

}
