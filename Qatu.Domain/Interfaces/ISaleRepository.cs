using Qatu.Domain.Entities;

namespace Qatu.Domain.Interfaces
{
    public interface ISaleRepository
    {
        Task UpdateAsync(Sale sale);
        Task<Sale?> GetByIdAsync(Guid id);
        Task<Sale?> GetByChatIdAsync(Guid chatId);
    }
}
