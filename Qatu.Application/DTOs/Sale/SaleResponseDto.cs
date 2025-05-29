using Qatu.Domain.Enums;

namespace Qatu.Application.DTOs.Sale
{
    public class SaleResponseDto
    {
        public Guid Id { get; set; }
        public Guid ChatId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid ProductId { get; set; }
        public SaleStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
