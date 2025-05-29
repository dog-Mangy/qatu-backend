namespace Qatu.Application.DTOs.Request
{
    public class CreateRequestDto
    {
        public Guid UserId { get; set; }
        public string Description { get; set; } = null!;
    }
}
