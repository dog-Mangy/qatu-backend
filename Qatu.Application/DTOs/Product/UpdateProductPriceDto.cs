namespace Qatu.Application.DTOs.Product
{
    public class UpdateProductPriceDto
    {
        public int ProductId { get; set; }
        public decimal NewPrice { get; set; }
    }
}