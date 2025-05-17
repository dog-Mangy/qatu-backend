namespace Qatu.Application.DTOs.Category
{
    public class GetCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;

        public string ImageUrl { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
    }
}
