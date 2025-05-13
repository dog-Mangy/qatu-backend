using Qatu.Application.DTOs.Store;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Stores
{
    public class UpdateStoreUseCase
    {
        private readonly IStoreRepository _repository;

        public UpdateStoreUseCase(IStoreRepository repository)
        {
            _repository = repository;
        }

        public async Task<Store?> HandleAsync(Guid storeId, UpdateStoreDto command)
        {
            var store = await _repository.GetByIdAsync(storeId);

            if (store == null)
            {
                return null;
            }

            store.Name = command.Name;
            store.Description = command.Description;

            await _repository.UpdateAsync(store);

            return store;
        }
    }
}
