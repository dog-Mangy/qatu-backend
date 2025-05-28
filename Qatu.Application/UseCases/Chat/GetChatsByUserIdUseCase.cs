using Qatu.Application.DTOs.Chat;

using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Chat
{
    public class GetChatsByUserIdUseCase
    {
        private readonly IChatRepository _chatRepository;

        public GetChatsByUserIdUseCase(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<IEnumerable<ChatResponseDto>> ExecuteAsync(Guid userId)
        {
            var chats = await _chatRepository.GetChatsByUserIdAsync(userId);
            return chats.Select(c => new ChatResponseDto
            {
                Id = c.Id,
                BuyerId = c.BuyerId,
                SellerId = c.SellerId,
                ProductId = c.ProductId,
            });
        }
    }
}
