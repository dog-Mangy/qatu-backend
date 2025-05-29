namespace Qatu.Application.DTOs.Chat
{
    public class CreateMessageRequestDto
    {
        public Guid SenderId { get; set; }
        public string Content { get; set; } = null!;
    }
}
