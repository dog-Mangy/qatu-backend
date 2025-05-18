using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Qatu.Application.DTOs.Category;
using Qatu.Application.UseCases.Categories;

namespace Qatu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CreateCategoryUseCase _createCategory;
        private readonly GetAllCategoriesUseCase _getAllCategoriesUseCase;
        private readonly GetCategoryByIdUseCase _getCategoryById;
        private readonly UpdateCategoryUseCase _updateCategory;
        private readonly DeleteCategoryUseCase _deleteCategory;

        public CategoriesController(
            CreateCategoryUseCase createCategory,
            GetCategoryByIdUseCase getCategoryById,
            GetAllCategoriesUseCase getAllCategoriesUseCase,
            UpdateCategoryUseCase updateCategory,
            DeleteCategoryUseCase deleteCategory)
        {
            _createCategory = createCategory;
            _getAllCategoriesUseCase = getAllCategoriesUseCase;
            _getCategoryById = getCategoryById;
            _updateCategory = updateCategory;
            _deleteCategory = deleteCategory;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(Guid id)
        {
            var category = await _getAllCategoriesUseCase.ExecuteAsync();
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "UserOnly")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _getCategoryById.ExecuteAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            var createdCategory = await _createCategory.HandleAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto dto)
        {
            var updatedCategory = await _updateCategory.HandleAsync(id, dto);
            if (updatedCategory == null)
                return NotFound();

            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _deleteCategory.ExecuteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
