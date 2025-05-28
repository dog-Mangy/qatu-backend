using Microsoft.EntityFrameworkCore;

using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

namespace Qatu.Infrastructure.Persistence.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly QatuDbContext _context;

        public MessageRepository(QatuDbContext context)
        {
            _context = context;
        }

        public async Task<Message> CreateMessageAsync(Message message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var chatExists = await _context.Chats.AnyAsync(c => c.Id == message.ChatId);
            if (!chatExists)
            {
                throw new ArgumentException("Chat does not exist.", nameof(message.ChatId));
            }

            var chat = await _context.Chats.FirstOrDefaultAsync(c => c.Id == message.ChatId);
            if (chat?.BuyerId != message.SenderId && chat?.SellerId != message.SenderId)
            {
                throw new ArgumentException("Sender is not a participant in this chat.", nameof(message.SenderId));
            }

            message.Id = Guid.NewGuid();
            message.SentAt = DateTime.UtcNow;

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return message;
        }

        public async Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }
    }
}
