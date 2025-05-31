using Qatu.Domain.Enums;

namespace Qatu.Domain.Entities
{
    public class Request
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public string StoreName { get; set; } = null!;
        public string StoreDescription { get; set; } = null!;

        public string Description { get; set; } = null!;

        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}
