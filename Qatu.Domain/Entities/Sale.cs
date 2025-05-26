using Qatu.Domain.Enums;

namespace Qatu.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ChatId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid ProductId { get; set; }
        public SaleStatus Status { get; set; } = SaleStatus.Waiting;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public Chat Chat { get; set; } = null!;
        public User Buyer { get; set; } = null!;
        public User Seller { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
