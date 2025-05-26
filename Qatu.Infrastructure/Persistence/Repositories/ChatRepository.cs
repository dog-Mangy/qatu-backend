using Microsoft.EntityFrameworkCore;

using Qatu.Domain.Entities;
using Qatu.Domain.Enums;
using Qatu.Domain.Interfaces;

namespace Qatu.Infrastructure.Persistence.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly QatuDbContext _context;

        public ChatRepository(QatuDbContext context)
        {
            _context = context;
        }

        public async Task<Chat> CreateChatAsync(Guid buyerId, Guid productId)
        {
            // Validate BuyerId exists
            var buyerExists = await _context.Users.AnyAsync(u => u.Id == buyerId);
            if (!buyerExists)
            {
                throw new ArgumentException("Buyer does not exist.", nameof(buyerId));
            }

            // Get StoreId and SellerId from Product
            var product = await _context.Products
                .Include(p => p.Store)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null)
            {
                throw new ArgumentException("Product does not exist.", nameof(productId));
            }

            var sellerId = product.Store.UserId;

            // Create Chat
            var chat = new Chat
            {
                Id = Guid.NewGuid(),
                BuyerId = buyerId,
                SellerId = sellerId,
                ProductId = productId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Chats.Add(chat);

            // Create Sale
            var sale = new Sale
            {
                Id = Guid.NewGuid(),
                ChatId = chat.Id,
                BuyerId = buyerId,
                SellerId = sellerId,
                ProductId = productId,
                Status = SaleStatus.Waiting,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Sales.Add(sale);

            await _context.SaveChangesAsync();

            return chat;
        }

        public async Task<IEnumerable<Chat>> GetChatsByUserIdAsync(Guid userId)
        {
            return await _context.Chats
                .Where(c => c.BuyerId == userId || c.SellerId == userId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}
