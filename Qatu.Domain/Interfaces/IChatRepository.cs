using Qatu.Domain.Entities;

namespace Qatu.Domain.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat> CreateChatAsync(Guid buyerId, Guid productId);
        Task<IEnumerable<Chat>> GetChatsByUserIdAsync(Guid userId);
    }
}
