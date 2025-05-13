namespace Qatu.Application.DTOs.Product
{
    public class CreateProductListDto
    {
        public List<CreateProductDto> Products { get; set; } = new();
    }
}
