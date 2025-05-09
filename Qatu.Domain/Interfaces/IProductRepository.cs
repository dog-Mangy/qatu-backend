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
        Task<int> CountAsync(Guid? storeId = null);
        Task<List<Product>> GetPagedFilteredAndSortedAsync(
            string? category,
            decimal? minPrice,
            decimal? maxPrice,
            decimal? minRating,
            decimal? maxRating,
            string? sortBy,
            bool ascending,
            int page,
            int pageSize,
            Guid? storeId = null
            );
    }
}