
using Microsoft.AspNetCore.Mvc;

using Qatu.Application.DTOs.Sale;
using Qatu.Application.UseCases.Sale;

namespace Qatu.API.Controllers.Sale
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly UpdateSaleUseCase _updateSaleUseCase;
        private readonly GetSaleByIdUseCase _getSaleByIdUseCase;
        private readonly GetSaleByChatIdUseCase _getSaleByChatIdUseCase;
        private readonly CheckSaleRelationshipUseCase _checkSaleRelationshipUseCase;

        public SalesController(
            UpdateSaleUseCase updateSaleUseCase,
            GetSaleByIdUseCase getSaleByIdUseCase,
            GetSaleByChatIdUseCase getSaleByChatIdUseCase,
            CheckSaleRelationshipUseCase checkSaleRelationshipUseCase)
        {
            _updateSaleUseCase = updateSaleUseCase;
            _getSaleByIdUseCase = getSaleByIdUseCase;
            _getSaleByChatIdUseCase = getSaleByChatIdUseCase;
            _checkSaleRelationshipUseCase = checkSaleRelationshipUseCase;
        }

        [HttpPut("{saleId}")]
        public async Task<IActionResult> UpdateSale(Guid saleId, [FromBody] UpdateSaleDto request)
        {
            await _updateSaleUseCase.ExecuteAsync(saleId, request);
            return NoContent();
        }

        [HttpGet("{saleId}")]
        public async Task<IActionResult> GetSaleById(Guid saleId)
        {
            var sale = await _getSaleByIdUseCase.ExecuteAsync(saleId);
            if (sale == null)
            {
                return NotFound("Sale not found.");
            }
            return Ok(sale);
        }

        [HttpGet("by-chat/{chatId}")]
        public async Task<IActionResult> GetSaleByChatId(Guid chatId)
        {
            var sale = await _getSaleByChatIdUseCase.ExecuteAsync(chatId);
            if (sale == null)
            {
                return NotFound("Sale not found for the specified chat.");
            }
            return Ok(sale);
        }

        [HttpGet("check-relationship")]
        public async Task<IActionResult> CheckRelationship([FromQuery] Guid userId, [FromQuery] Guid relatedId)
        {
            var result = await _checkSaleRelationshipUseCase.ExecuteAsync(userId, relatedId);
            return Ok(result);
        }
    }
}
