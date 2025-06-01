using Qatu.Domain.Enums;
using System.Text.Json.Serialization;


namespace Qatu.Application.DTOs.Request
{
    public class GetRequestDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public string StoreName { get; set; } = null!;

        public string StoreDescription { get; set; } = null!;

        public string Description { get; set; } = null!;

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RequestStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
