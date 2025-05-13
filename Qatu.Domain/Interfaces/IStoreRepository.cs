using Qatu.Domain.Entities;

namespace Qatu.Domain.Interfaces
{
    public interface IStoreRepository
    {
        Task<Store?> GetByIdAsync(Guid id);
        Task<IEnumerable<Store>> GetAllAsync();
        Task<IEnumerable<Store>> GetByUserIdAsync(Guid userId);
        Task AddAsync(Store store);
        Task UpdateAsync(Store store);
        Task DeleteAsync(Guid id);
    }
}
