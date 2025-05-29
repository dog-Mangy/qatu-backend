using Qatu.Domain.Entities;

namespace Qatu.Domain.Interfaces
{
    public interface IRequestRepository
    {
        Task<Request?> GetByIdAsync(Guid id);
        Task<IEnumerable<Request>> GetAllAsync();
        Task<IEnumerable<Request>> GetByUserIdAsync(Guid userId);
        Task AddAsync(Request request);
        Task UpdateAsync(Request request);
        Task DeleteAsync(Guid id);
    }
}
