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

        public RequestsController(
            GetAllRequestsUseCase getAllRequestsUseCase,
            CreateRequestUseCase createRequestUseCase,
            DeleteRequestUseCase deleteRequestUseCase)
        {
            _getAllRequestsUseCase = getAllRequestsUseCase;
            _createRequestUseCase = createRequestUseCase;
            _deleteRequestUseCase = deleteRequestUseCase;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll(Guid id)
        {
            var category = await _getAllRequestsUseCase.ExecuteAsync();
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRequestDto dto)
        {
            var createdRequest = await _createRequestUseCase.HandleAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = createdRequest.Id }, createdRequest);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _deleteRequestUseCase.ExecuteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
        
    }
}
