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
    }
}
