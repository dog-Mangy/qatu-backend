using Qatu.Domain.Entities;

namespace Qatu.Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId);
    }
}
