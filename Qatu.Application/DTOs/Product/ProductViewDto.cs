namespace Qatu.Application.DTOs.Product
{
    public class ProductViewDto
    {
        public Guid Id { get; set; }
        public Guid StoreId { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string? Description { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public int Stock { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
