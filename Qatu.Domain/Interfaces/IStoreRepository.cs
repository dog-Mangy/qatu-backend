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
        Task<int> CountAsync(Guid? userId = null);
        Task<List<Store>> GetPagedFilteredAndSortedAsync(
            Guid? userId,
            string? sortBy,
            string? searchQuery,
            bool ascending,
            int page,
            int pageSize);
    }
}
