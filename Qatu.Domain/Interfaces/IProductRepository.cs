using Qatu.Domain.Entities;

namespace Qatu.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(Guid id);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> AddAsyncRange(List<Product> products);
        Task<IEnumerable<Product>> GetByStoreIdAsync(Guid storeId);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Guid id);
        Task<int> CountAsync();
        Task<int> CountByStoreAsync(Guid storeId);
        Task<List<Product>> GetPagedAsync(int page, int pageSize);
        Task<List<Product>> GetPagedByStoreAsync(Guid storeId, int page, int pageSize);
    }
}
