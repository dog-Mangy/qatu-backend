using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Stores
{
    public class DeleteStoreUseCase
    {
        private readonly IStoreRepository _repository;

        public DeleteStoreUseCase(IStoreRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(Guid storeId)
        {
            var existingStore = await _repository.GetByIdAsync(storeId);
            if (existingStore == null)
                return false;

            await _repository.DeleteAsync(storeId);
            return true;
        }
    }
}
