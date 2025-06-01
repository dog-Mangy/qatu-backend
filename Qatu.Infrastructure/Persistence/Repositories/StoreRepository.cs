using Microsoft.EntityFrameworkCore;

using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;
using Qatu.Infrastructure.Persistence;

namespace Qatu.Infrastructure.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly QatuDbContext _context;

        public StoreRepository(QatuDbContext context)
        {
            _context = context;
        }

        public async Task<Store?> GetByIdAsync(Guid id)
        {
            return await _context.Stores
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<IEnumerable<Store>> GetAllAsync()
        {
            return await _context.Stores
                .ToListAsync();
        }

        public async Task<IEnumerable<Store>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Stores
                .Where(s => s.UserId == userId)
                .ToListAsync();
        }

        public async Task AddAsync(Store store)
        {
            await _context.Stores.AddAsync(store);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Store store)
        {
            _context.Stores.Update(store);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store != null)
            {
                _context.Stores.Remove(store);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> CountAsync(Guid? userId = null)
        {
            if (userId.HasValue)
            {
                return await _context.Stores
                    .Where(s => s.UserId == userId.Value)
                    .CountAsync();
            }
            return await _context.Stores.CountAsync();
        }

        public async Task<List<Store>> GetPagedFilteredAndSortedAsync(
            Guid? userId,
            string? sortBy,
            string? searchQuery,
            bool ascending,
            int page,
            int pageSize)
        {
            var query = _context.Stores.AsQueryable();

            if (userId.HasValue)
            {
                query = query.Where(s => s.UserId == userId.Value);
            }

            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(s => 
                    s.Name.Contains(searchQuery) || 
                    (s.Description ?? string.Empty).Contains(searchQuery));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                query = ascending
                    ? query.OrderBy(s => EF.Property<object>(s, sortBy))
                    : query.OrderByDescending(s => EF.Property<object>(s, sortBy));
            }
            else
            {
                query = ascending 
                    ? query.OrderBy(s => s.CreatedAt) 
                    : query.OrderByDescending(s => s.CreatedAt);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}