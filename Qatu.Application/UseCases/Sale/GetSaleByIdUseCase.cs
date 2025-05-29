using Qatu.Application.DTOs.Sale;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Sale
{
    public class GetSaleByIdUseCase
    {
        private readonly ISaleRepository _saleRepository;

        public GetSaleByIdUseCase(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<SaleResponseDto?> ExecuteAsync(Guid saleId)
        {
            var sale = await _saleRepository.GetByIdAsync(saleId);
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
