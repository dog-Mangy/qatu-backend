using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Qatu.Infrastructure.Persistence;

namespace Qatu.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly QatuDbContext _context;

        public ProductRepository(QatuDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByStoreIdAsync(Guid storeId)
        {
            return await _context.Products
                .Where(p => p.StoreId == storeId)
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> AddAsyncRange(List<Product> products)
        {
            await _context.Products.AddRangeAsync(products);
            await _context.SaveChangesAsync();

            _context.ChangeTracker.Clear();

            return products;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<int> CountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<int> CountAsync(Guid? storeId = null)
        {
            int result;
            if (storeId.HasValue)
            {
                result = await _context.Products
                .Where(p => p.StoreId == storeId)
                .CountAsync();
            } else
            {
                result = await _context.Products.CountAsync();
            }
            return result;
        }

        public async Task<List<Product>> GetPagedFilteredAndSortedAsync(
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
)
        {
            var query = _context.Products.AsQueryable();

            if (storeId.HasValue)
            {
                query = query.Where(p => p.StoreId == storeId.Value);
            }

            if (!string.IsNullOrEmpty(category))
                query = query.Where(p => p.Category == category);

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice.Value);

            if (minRating.HasValue)
                query = query.Where(p => p.Rating >= minRating.Value);

            if (maxRating.HasValue)
                query = query.Where(p => p.Rating <= maxRating.Value);

            if (!string.IsNullOrEmpty(sortBy))
            {
                query = ascending
                    ? query.OrderBy(p => EF.Property<object>(p, sortBy))
                    : query.OrderByDescending(p => EF.Property<object>(p, sortBy));
            }
            else
            {
                query = ascending ? query.OrderBy(p => p.Id) : query.OrderByDescending(p => p.Id);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
