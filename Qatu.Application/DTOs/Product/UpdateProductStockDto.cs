namespace Qatu.Application.DTOs.Product
{
    public class UpdateProductStockDto
    {
        public Guid ProductId { get; set; }
        public int NewStock { get; set; }
    }
}
