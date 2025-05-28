using Qatu.Application.DTOs.Chat;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Chat
{
    public class CreateMessageUseCase
    {
        private readonly IMessageRepository _messageRepository;

        public CreateMessageUseCase(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task<MessageResponseDto> ExecuteAsync(Guid chatId, CreateMessageRequestDto request)
        {
            var message = new Message
            {
                ChatId = chatId,
                SenderId = request.SenderId,
                Content = request.Content
            };

            var createdMessage = await _messageRepository.CreateMessageAsync(message);

            return new MessageResponseDto
            {
                Id = createdMessage.Id,
                ChatId = createdMessage.ChatId,
                SenderId = createdMessage.SenderId,
                Content = createdMessage.Content,
                SentAt = createdMessage.SentAt
            };
        }
    }
}
