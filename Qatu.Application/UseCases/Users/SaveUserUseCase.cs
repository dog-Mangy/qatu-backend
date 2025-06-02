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

        public async Task<User> ExecuteAsync(SaveUserDTO userDTO)
        {
            var existingUser = await _repository.GetByIdAsync(userDTO.Id);
            if (existingUser != null)
            {
                throw new Exception("User already exists");
            }

            var userToCreate = new User
            {
                Id = userDTO.Id,
                Name = userDTO.Name,
                Role = userDTO.Role,
                CreatedAt = userDTO.CreatedAt,
                Email = userDTO.Email
            };

            try
            {
                await _repository.AddAsync(userToCreate);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new Exception("User could not be created");
            }

            var userCreated = await _repository.GetByIdAsync(userDTO.Id);
            if (userCreated == null)
            {
                throw new Exception("Error creating user");
            }

            return userCreated;
        }
    }
}
