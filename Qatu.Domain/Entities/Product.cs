using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Qatu.Domain.Entities
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid StoreId { get; set; }
        public Guid CategoryId { get; set; } 

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; } = 0.0m;
        public int Stock { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public Store Store { get; set; } = null!;

        [JsonIgnore]
        public Category Category { get; set; } = null!; // <-- navegación
    }
}
