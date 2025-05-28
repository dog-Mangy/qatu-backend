using Microsoft.AspNetCore.Mvc;

using Qatu.Application.DTOs.Chat;
using Qatu.Application.UseCases.Chat;

namespace Qatu.API.Controllers.Chat
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatsController : ControllerBase
    {
        private readonly CreateChatUseCase _createChatUseCase;
        private readonly GetChatsByUserIdUseCase _getChatsByUserIdUseCase;
        private readonly GetMessagesByChatIdUseCase _getMessagesByChatIdUseCase;
        private readonly CreateMessageUseCase _createMessageUseCase;

        public ChatsController(
            CreateChatUseCase createChatUseCase,
            GetChatsByUserIdUseCase getChatsByUserIdUseCase,
            GetMessagesByChatIdUseCase getMessagesByChatIdUseCase,
            CreateMessageUseCase createMessageUseCase)
        {
            _createChatUseCase = createChatUseCase;
            _getChatsByUserIdUseCase = getChatsByUserIdUseCase;
            _getMessagesByChatIdUseCase = getMessagesByChatIdUseCase;
            _createMessageUseCase = createMessageUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatRequestDto request)
        {
            var chat = await _createChatUseCase.ExecuteAsync(request);
            return CreatedAtAction(nameof(GetChatMessages), new { chatId = chat.Id }, chat);
        }

        [HttpGet]
        public async Task<IActionResult> GetChatsByUser([FromQuery] Guid userId)
        {
            var chats = await _getChatsByUserIdUseCase.ExecuteAsync(userId);
            return Ok(chats);
        }

        [HttpGet("{chatId}/messages")]
        public async Task<IActionResult> GetChatMessages(Guid chatId)
        {
            var messages = await _getMessagesByChatIdUseCase.ExecuteAsync(chatId);
            if (!messages.Any())
            {
                return NotFound("No messages found for the specified chat.");
            }
            return Ok(messages);
        }

        [HttpPost("{chatId}/messages")]
        public async Task<IActionResult> CreateMessage(Guid chatId, [FromBody] CreateMessageRequestDto request)
        {
            var message = await _createMessageUseCase.ExecuteAsync(chatId, request);
            return CreatedAtAction(nameof(GetChatMessages), new { chatId = message.ChatId }, message);
        }
    }
}
