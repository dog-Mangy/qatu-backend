using Qatu.Application.DTOs.Category;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Categories
{
    public class GetCategoryByIdUseCase
    {
        private readonly ICategoryRepository _repository;

        public GetCategoryByIdUseCase(ICategoryRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<GetCategoryDto?> ExecuteAsync(Guid storeId)
        {
            var category = await _repository.GetByIdAsync(storeId);

            if (category == null)
                return null;

            return new GetCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImageUrl = category.ImageUrl,
                CreatedAt = category.CreatedAt
            };
        }
    }
}
