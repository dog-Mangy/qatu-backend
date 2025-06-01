namespace Qatu.Application.DTOs.Request
{
    public class CreateRequestDto
    {
        public Guid UserId { get; set; }
        public string StoreName { get; set; } = null!;
        public string StoreDescription { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
