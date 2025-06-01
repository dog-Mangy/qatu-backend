using Qatu.Application.DTOs;
using Qatu.Application.DTOs.Store;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Stores
{
    public class GetStoresPagedUseCase
    {
        private readonly IStoreRepository _repository;

        public GetStoresPagedUseCase(IStoreRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetStoreDto>> ExecuteAsync()
        {
            var stores = await _repository.GetAllAsync();

            return stores.Select(store => new GetStoreDto
            {
                Id = store.Id,
                UserId = store.UserId,
                Name = store.Name,
                Description = store.Description,
                CreatedAt = store.CreatedAt
            });
        }

        public async Task<PagedResult<GetStoreDto>> ExecutePagedAsync(
            int page,
            int pageSize,
            Guid? userId = null,
            string? sortBy = null,
            string? searchQuery = null,
            bool ascending = true)
        {
            var totalStores = await _repository.CountAsync(userId);
            var stores = await _repository.GetPagedFilteredAndSortedAsync(
                userId, sortBy, searchQuery, ascending, page, pageSize);

            var totalPages = (int)Math.Ceiling((double)totalStores / pageSize);

            return new PagedResult<GetStoreDto>
            {
                Items = stores.Select(store => new GetStoreDto
                {
                    Id = store.Id,
                    UserId = store.UserId,
                    Name = store.Name,
                    Description = store.Description,
                    CreatedAt = store.CreatedAt
                }).ToList(),
                Page = page,
                PageSize = pageSize,
                HasNext = page < totalPages,
                HasPrevious = page > 1 && totalPages >= 1,
                NPages = totalPages,
                NElements = totalStores
            };
        }
    }
}