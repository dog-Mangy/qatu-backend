using Qatu.Application.DTOs.Sale;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Sale
{
    public class GetSaleByChatIdUseCase
    {
        private readonly ISaleRepository _saleRepository;

        public GetSaleByChatIdUseCase(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<SaleResponseDto?> ExecuteAsync(Guid chatId)
        {
            var sale = await _saleRepository.GetByChatIdAsync(chatId);
            if (sale == null)
            {
                return null;
            }

            return new SaleResponseDto
            {
                Id = sale.Id,
                ChatId = sale.ChatId,
                BuyerId = sale.BuyerId,
                SellerId = sale.SellerId,
                ProductId = sale.ProductId,
                Status = sale.Status,
                CreatedAt = sale.CreatedAt,
                UpdatedAt = sale.UpdatedAt
            };
        }
    }
}
