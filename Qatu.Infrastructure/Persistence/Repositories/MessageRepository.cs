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

        public async Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId)
        {
            return await _context.Messages
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.SentAt)
                .ToListAsync();
        }
    }
}
