using Qatu.Application.DTOs.Chat;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Chat
{
    public class GetMessagesByChatIdUseCase
    {
        private readonly IMessageRepository _messageRepository;

        public GetMessagesByChatIdUseCase(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<IEnumerable<MessageResponseDto>> ExecuteAsync(Guid chatId)
        {
            var messages = await _messageRepository.GetMessagesByChatIdAsync(chatId);
            return messages.Select(m => new MessageResponseDto
            {
                Id = m.Id,
                ChatId = m.ChatId,
                SenderId = m.SenderId,
                Content = m.Content,
                SentAt = m.SentAt
            });
        }
    }
}
