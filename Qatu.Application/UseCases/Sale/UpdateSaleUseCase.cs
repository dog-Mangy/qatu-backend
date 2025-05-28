using Qatu.Application.DTOs.Sale;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Sale
{
    public class UpdateSaleUseCase
    {
        private readonly ISaleRepository _saleRepository;

        public UpdateSaleUseCase(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task ExecuteAsync(Guid saleId, UpdateSaleDto request)
        {
            var sale = await _saleRepository.GetByIdAsync(saleId);
            if (sale == null)
            {
                throw new ArgumentException("Sale not found.", nameof(saleId));
            }

            sale.Status = request.Status;
            await _saleRepository.UpdateAsync(sale);
        }
    }
}
