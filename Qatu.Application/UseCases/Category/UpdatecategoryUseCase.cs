using Qatu.Application.DTOs.Category;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Categories
{
    public class UpdateCategoryUseCase
    {
        private readonly ICategoryRepository _repository;

        public UpdateCategoryUseCase(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Category?> HandleAsync(Guid categoryId, UpdateCategoryDto command)
        {
            var category = await _repository.GetByIdAsync(categoryId);

            if (category == null)
            {
                return null;
            }

            category.Name = command.Name;
            category.Description = command.Description;
            category.ImageUrl = command.ImageUrl;

            await _repository.UpdateAsync(category);

            return category;
        }
    }
}
