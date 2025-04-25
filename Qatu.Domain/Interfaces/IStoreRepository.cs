using Qatu.Domain.Entities;

namespace Qatu.Domain.Interfaces
{
    public interface IStoreRepository
    {
        Task<Store?> GetByIdAsync(int id);
        Task<IEnumerable<Store>> GetAllAsync();
        Task<IEnumerable<Store>> GetByUserIdAsync(int userId);
        Task AddAsync(Store store);
        Task UpdateAsync(Store store);
        Task DeleteAsync(int id);
    }
}
