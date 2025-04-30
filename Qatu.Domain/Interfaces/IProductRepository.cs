using Qatu.Domain.Entities;

namespace Qatu.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> AddAsyncRange(List<Product> products);
        Task<IEnumerable<Product>> GetByStoreIdAsync(int storeId);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
        Task<int> CountByStoreAsync(int storeId);
        Task<List<Product>> GetPagedAsync(int page, int pageSize);
        Task<List<Product>> GetPagedByStoreAsync(int storeId, int page, int pageSize);
    }
}
