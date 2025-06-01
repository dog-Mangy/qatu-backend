using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Qatu.Application.DTOs.User;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Users
{
    public class SaveUserUseCase
    {
        private readonly IUserRepository _repository;
        public SaveUserUseCase(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> HandleAsync(Guid storeId, SaveUserDTO userDTO)
        {
            User user = new()
            {
                Id = userDTO.Id,
                Name = userDTO.Name,
                Role = userDTO.Role,
            };

            await _repository.AddAsync(user);

            return await _repository.GetByIdAsync(userDTO.Id); ;
        }
    }
}
