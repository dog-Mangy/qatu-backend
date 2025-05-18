using Qatu.Application.DTOs.Category;
using Qatu.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qatu.Application.UseCases.Categories
{
    public class GetAllCategoriesUseCase
    {
        private readonly ICategoryRepository _repository;

        public GetAllCategoriesUseCase(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetCategoryDto>> ExecuteAsync()
        {
            var categories = await _repository.GetAllAsync();

            return categories.Select(category => new GetCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImageUrl = category.ImageUrl,
                CreatedAt = category.CreatedAt
            }).ToList();
        }
    }
}
