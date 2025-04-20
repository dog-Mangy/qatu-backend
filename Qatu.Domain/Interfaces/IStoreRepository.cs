using Qatu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qatu.Domain.Interfaces
{
    public interface IStoreRepository
    {
        Task<Store?> GetByIdAsync(int id);
        Task<IEnumerable<Store>> GetAllAsync();
        Task<IEnumerable<Store>> GetByUserIdAsync(int userId);
        Task AddAsync(Store store);
        Task UpdateAsync(Store store);
        Task DeleteAsync(int id);
    }
}
