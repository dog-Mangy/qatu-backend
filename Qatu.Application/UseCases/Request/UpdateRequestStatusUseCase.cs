using Qatu.Application.DTOs.Request;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Requests
{
    public class UpdateRequestStatusUseCase
    {
        private readonly IRequestRepository _repository;

        public UpdateRequestStatusUseCase(IRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<Request?> HandleAsync(Guid requestId, UpdateRequestStatusDto dto)
        {
            var request = await _repository.GetByIdAsync(requestId);

            if (request == null)
            {
                return null;
            }

            request.Status = dto.Status;
            request.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(request);

            return request;
        }
    }
}
