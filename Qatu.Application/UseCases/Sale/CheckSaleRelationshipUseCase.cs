using Qatu.Application.DTOs.Sale;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Sale
{
    public class CheckSaleRelationshipUseCase
    {
        private readonly ISaleRepository _saleRepository;

        public CheckSaleRelationshipUseCase(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<CheckRelationshipResponseDto> ExecuteAsync(Guid userId, Guid relatedId)
        {
            var productSale = await _saleRepository.GetDoneSaleByUserAndProductAsync(userId, relatedId);
            if (productSale != null)
            {
                return new CheckRelationshipResponseDto
                {
                    ProductId = productSale.ProductId,
                    IsRelated = true
                };
            }

            var hasStoreSale = await _saleRepository.HasDoneSaleByUserAndStoreAsync(userId, relatedId);
            if (hasStoreSale)
            {
                return new CheckRelationshipResponseDto
                {
                    StoreId = relatedId,
                    IsRelated = true
                };
            }

            return new CheckRelationshipResponseDto
            {
                IsRelated = false
            };
        }
    }
}
