using Microsoft.EntityFrameworkCore;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;
using Qatu.Infrastructure.Persistence;

namespace Qatu.Infrastructure.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly QatuDbContext _context;

        public RequestRepository(QatuDbContext context)
        {
            _context = context;
        }

        public async Task<Request?> GetByIdAsync(Guid id)
        {
            return await _context.Requests
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Request>> GetAllAsync()
        {
            return await _context.Requests
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task<IEnumerable<Request>> GetByUserIdAsync(Guid userId)
        {
            return await _context.Requests
                .Where(r => r.UserId == userId)
                .Include(r => r.User)
                .ToListAsync();
        }

        public async Task AddAsync(Request request)
        {
            await _context.Requests.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Request request)
        {
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var request = await _context.Requests.FindAsync(id);
            if (request != null)
            {
                _context.Requests.Remove(request);
                await _context.SaveChangesAsync();
            }
        }
    }
}
