using Qatu.Domain.Interfaces;


namespace Qatu.Application.UseCases.Requests
{
    public class DeleteRequestUseCase
    {
        private readonly IRequestRepository _repository;

        public DeleteRequestUseCase(IRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ExecuteAsync(Guid requestId)
        {
            var existingRequest = await _repository.GetByIdAsync(requestId);
            if (existingRequest == null)
                return false;

            await _repository.DeleteAsync(requestId);
            return true;
        }
    }
}
