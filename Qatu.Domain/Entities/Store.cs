namespace Qatu.Domain.Entities
{
    public class Store
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public User User { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}

