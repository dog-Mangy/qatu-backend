using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Qatu.Application.DTOs.Request;
using Qatu.Application.UseCases.Requests;

namespace Qatu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RequestsController : ControllerBase
    {
        private readonly GetAllRequestsUseCase _getAllRequestsUseCase;
        private readonly CreateRequestUseCase _createRequestUseCase;


        private readonly DeleteRequestUseCase _deleteRequestUseCase;
        private readonly UpdateRequestStatusUseCase _updateRequestStatusUseCase;

        public RequestsController(
            GetAllRequestsUseCase getAllRequestsUseCase,
            CreateRequestUseCase createRequestUseCase,
            DeleteRequestUseCase deleteRequestUseCase,
            UpdateRequestStatusUseCase updateRequestStatusUseCase)
        {
            _getAllRequestsUseCase = getAllRequestsUseCase;
            _createRequestUseCase = createRequestUseCase;
            _deleteRequestUseCase = deleteRequestUseCase;
            _updateRequestStatusUseCase = updateRequestStatusUseCase;
        }


        [HttpGet]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetAll(Guid id)
        {
            var category = await _getAllRequestsUseCase.ExecuteAsync();
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Create([FromBody] CreateRequestDto dto)
        {
            var createdRequest = await _createRequestUseCase.HandleAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = createdRequest.Id }, createdRequest);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _deleteRequestUseCase.ExecuteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}/status")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateRequestStatusDto dto)
        {
            var updatedRequest = await _updateRequestStatusUseCase.HandleAsync(id, dto);

            if (updatedRequest == null)
                return NotFound();

            return Ok(updatedRequest);
        }
        
    }
}
