using Qatu.Application.DTOs.Request;
using Qatu.Domain.Interfaces;


namespace Qatu.Application.UseCases.Requests
{
    public class GetAllRequestsUseCase
    {
        private readonly IRequestRepository _repository;

        public GetAllRequestsUseCase(IRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetRequestDto>> ExecuteAsync()
        {
            var requests = await _repository.GetAllAsync();

            return requests.Select(request => new GetRequestDto
            {
                Id = request.Id,
                UserId = request.UserId,
                StoreName = request.StoreName,
                StoreDescription = request.StoreDescription,
                Description = request.Description,
                Status = request.Status,
                CreatedAt = request.CreatedAt,
                UpdatedAt = request.UpdatedAt
            }).ToList();
        }
    }
}
