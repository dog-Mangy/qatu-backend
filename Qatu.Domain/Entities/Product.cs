using System.ComponentModel.DataAnnotations.Schema;


namespace Qatu.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("store_id")]
        public Guid StoreId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Rating { get; set; } = 0.0m;
        public int Stock { get; set; } = 0;

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Store Store { get; set; } = null!;
    }
}
