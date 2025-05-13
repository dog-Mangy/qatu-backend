using Qatu.Domain.Entities;
using Qatu.Application.DTOs.Store;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Stores
{
    public class CreateStoreUseCase
    {
        private readonly IStoreRepository _repository;

        public CreateStoreUseCase(IStoreRepository repository)
        {
            _repository = repository;
        }

        public async Task<Store> HandleAsync(CreateStoreDto command)
        {
            Store store = new()
            {
                UserId = command.UserId,
                Name = command.Name,
                Description = command.Description
            };

            await _repository.AddAsync(store);

            return store;
        }
    }
}
