namespace Qatu.Domain.Entities
{
    public class Chat
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid ProductId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public User Buyer { get; set; } = null!;
        public User Seller { get; set; } = null!;
        public Product Product { get; set; } = null!;
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public Sale Sale { get; set; } = null!;
    }
}
