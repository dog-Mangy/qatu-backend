using System.ComponentModel.DataAnnotations;

namespace Qatu.Application.DTOs.Product
{
    public class CreateProductDto
    {
        [Required]
        public Guid StoreId { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string? Description { get; set; }

        [Required]
        public Guid CategoryId { get; set; } 

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int? Stock { get; set; }
    }

}
