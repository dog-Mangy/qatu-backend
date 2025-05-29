using Qatu.Application.DTOs.Request;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;
using System.Threading.Tasks;

namespace Qatu.Application.UseCases.Requests
{
    public class CreateRequestUseCase
    {
        private readonly IRequestRepository _repository;

        public CreateRequestUseCase(IRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<Request> HandleAsync(CreateRequestDto command)
        {
            Request request = new()
            {
                UserId = command.UserId,
                Description = command.Description,
                Status = Qatu.Domain.Enums.RequestStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(request);

            return request;
        }
    }
}
