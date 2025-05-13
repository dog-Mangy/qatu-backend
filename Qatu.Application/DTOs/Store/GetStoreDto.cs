namespace Qatu.Application.DTOs.Store
{
    public class GetStoreDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}
