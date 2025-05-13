namespace Qatu.Application.DTOs.Store
{
    public class CreateStoreDto
    {
        public Guid UserId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
