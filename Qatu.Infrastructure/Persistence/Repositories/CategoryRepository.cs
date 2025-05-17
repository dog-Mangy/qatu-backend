using Microsoft.EntityFrameworkCore;

using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;
using Qatu.Infrastructure.Persistence;

namespace Qatu.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly QatuDbContext _context;

        public CategoryRepository(QatuDbContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .ToListAsync();
        }

        public async Task AddAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
