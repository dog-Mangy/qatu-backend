using Qatu.Application.DTOs.Chat;

using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Chat
{
    public class CreateChatUseCase
    {
        private readonly IChatRepository _chatRepository;

        public CreateChatUseCase(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }

        public async Task<ChatResponseDto> ExecuteAsync(CreateChatRequestDto request)
        {
            var chat = await _chatRepository.CreateChatAsync(request.BuyerId, request.ProductId);
            return new ChatResponseDto
            {
                Id = chat.Id,
                BuyerId = chat.BuyerId,
                SellerId = chat.SellerId,
                ProductId = chat.ProductId,
            };
        }
    }
}
