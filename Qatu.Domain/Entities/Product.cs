using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qatu.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string Category { get; set; } = null!;
        public decimal Price { get; set; }
        public decimal Rating { get; set; } = 0.0m;
        public int Stock { get; set; } = 0;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Store Store { get; set; } = null!;
    }
}
