using Microsoft.EntityFrameworkCore;

using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

namespace Qatu.Infrastructure.Persistence.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly QatuDbContext _context;

        public SaleRepository(QatuDbContext context)
        {
            _context = context;
        }

        public async Task UpdateAsync(Sale sale)
        {
            if (sale == null)
            {
                throw new ArgumentNullException(nameof(sale));
            }

            var existingSale = await _context.Sales.FindAsync(sale.Id);
            if (existingSale == null)
            {
                throw new ArgumentException("Sale does not exist.", nameof(sale.Id));
            }

            existingSale.Status = sale.Status;
            existingSale.UpdatedAt = DateTime.UtcNow;

            // Update only mutable fields to prevent accidental changes to immutable fields
            _context.Sales.Update(existingSale);
            await _context.SaveChangesAsync();
        }

        public async Task<Sale?> GetByIdAsync(Guid id)
        {
            return await _context.Sales
                .Include(s => s.Buyer)
                .Include(s => s.Seller)
                .Include(s => s.Product)
                .Include(s => s.Chat)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Sale?> GetByChatIdAsync(Guid chatId)
        {
            return await _context.Sales
                .Include(s => s.Buyer)
                .Include(s => s.Seller)
                .Include(s => s.Product)
                .Include(s => s.Chat)
                .FirstOrDefaultAsync(s => s.ChatId == chatId);
        }
    }
}
