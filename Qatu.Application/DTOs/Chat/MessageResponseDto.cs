namespace Qatu.Application.DTOs.Chat
{
    public class MessageResponseDto
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; }
    }
}
