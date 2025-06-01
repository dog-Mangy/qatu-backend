using Microsoft.AspNetCore.Authorization;
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

        public UserController(
            SaveUserUseCase saveUserUseCase)
        {
            _saveUserUseCase = saveUserUseCase;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SaveUser()
        {
            var uuid = User.FindFirst("https://qatu.api/uuid")?.Value;
            var name = User.FindFirst("name")?.Value;
            var email = User.FindFirst("email")?.Value;
            var rolesClaim = User.FindFirst("https://qatu.api/roles")?.Value;

            List<string> roles = new List<string>();
            if (!string.IsNullOrEmpty(rolesClaim))
            {
                roles = System.Text.Json.JsonSerializer.Deserialize<List<string>>(rolesClaim);
            }

            var dto = new SaveUserDTO
            {
                Id = uuid != null ? Guid.Parse(uuid) : Guid.NewGuid(),
                Name = name ?? "",
                Email = email ?? "",
                Role = getRole(roles),
                CreatedAt = DateTime.UtcNow
            };


            return Ok(dto);
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
