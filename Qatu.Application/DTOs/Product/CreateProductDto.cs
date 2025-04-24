namespace Qatu.Application.DTOs.Product
{
    public class CreateProductDto
    {
        public int StoreId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
        public int? Stock { get; set; }
    }
}