using Qatu.Domain.Entities;

namespace Qatu.Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task<Message> CreateMessageAsync(Message message);
        Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId);
    }
}
