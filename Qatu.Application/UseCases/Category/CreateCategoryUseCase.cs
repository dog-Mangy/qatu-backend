using Qatu.Application.DTOs.Category;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Categories
{
    public class CreateCategoryUseCase
    {
        private readonly ICategoryRepository _repository;

        public CreateCategoryUseCase(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Category> HandleAsync(CreateCategoryDto command)
        {
            Category category = new()
            {

                Name = command.Name,
                Description = command.Description,
                ImageUrl = command.ImageUrl
            };


            await _repository.AddAsync(category);

            return category;
        }
    }
}
