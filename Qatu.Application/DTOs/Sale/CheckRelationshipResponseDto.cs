namespace Qatu.Application.DTOs.Sale
{
    public class CheckRelationshipResponseDto
    {
        public Guid? ProductId { get; set; }
        public Guid? StoreId { get; set; }
        public bool IsRelated { get; set; }
    }
}
