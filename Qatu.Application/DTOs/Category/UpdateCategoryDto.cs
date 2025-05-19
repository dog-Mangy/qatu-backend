using System.ComponentModel.DataAnnotations;

namespace Qatu.Application.DTOs.Category
{
    public class UpdateCategoryDto
    {

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

    }

}
