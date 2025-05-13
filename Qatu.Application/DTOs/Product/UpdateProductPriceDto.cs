namespace Qatu.Application.DTOs.Product
{
    public class UpdateProductPriceDto
    {
        public Guid ProductId { get; set; }
        public decimal NewPrice { get; set; }
    }
}
