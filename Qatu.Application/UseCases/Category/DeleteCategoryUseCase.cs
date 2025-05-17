using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Categories
{
    public class DeleteCategoryUseCase
    {
        private readonly ICategoryRepository _repository;

        public DeleteCategoryUseCase(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(Guid storeId)
        {
            var existingCategory = await _repository.GetByIdAsync(storeId);
            if (existingCategory == null)
                return false;

            await _repository.DeleteAsync(storeId);
            return true;
        }
    }
}
