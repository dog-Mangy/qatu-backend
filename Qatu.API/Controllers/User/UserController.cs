using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using Qatu.Application.DTOs.User;
using Qatu.Application.UseCases.Users;

using Qatu.Domain.Entities;
using Qatu.Domain.Enums;

namespace Qatu.API.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private SaveUserUseCase _saveUserUseCase;

        public UserController(SaveUserUseCase saveUserUseCase)
        {
            _saveUserUseCase = saveUserUseCase;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SaveUser([FromBody] SaveUserRequest body)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var uuid = User.FindFirst("https://qatu.api/uuid")?.Value;
                var roles = User.FindAll("https://qatu.api/roles").Select(c => c.Value).ToList();

                var dto = new SaveUserDTO
                {
                    Id = uuid != null ? Guid.Parse(uuid) : Guid.NewGuid(),
                    Name = body.Name ?? "",
                    Email = body.Email ?? "",
                    Role = getRole(roles),
                    CreatedAt = DateTime.UtcNow
                };

                var userCreated = await _saveUserUseCase.ExecuteAsync(dto);

                return Ok(userCreated);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new { error = ex.Message });
            }
        }




        private UserRole getRole(List<string>? roles)
        {
            if (roles == null || roles.Count == 0)
            {
                return UserRole.Buyer;
            }

            if (roles.Contains("Admin", StringComparer.OrdinalIgnoreCase))
            {
                return UserRole.Admin;
            }

            if (roles.Contains("Vendor", StringComparer.OrdinalIgnoreCase))
            {
                return UserRole.Seller;
            }

            return UserRole.Buyer;
        }

    }
}
