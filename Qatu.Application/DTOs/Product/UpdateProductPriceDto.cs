namespace Qatu.Application.DTOs
{
    public class UpdateProductPriceDto
    {
        public int ProductId { get; set; }
        public decimal NewPrice { get; set; }
    }
}