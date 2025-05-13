using Qatu.Domain.Enums;

namespace Qatu.Domain.Entities
{

    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Store> Stores { get; set; } = new List<Store>();
    }
}

