using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Qatu.Application.DTOs;
using Qatu.Application.DTOs.Product;
using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

namespace Qatu.Application.UseCases.Products
{
    public class GetProductsUseCase
    {
        private readonly IProductRepository _repository;

        public GetProductsUseCase(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<PagedResult<ProductViewDto>> ExecuteAsync(
            int page,
            int pageSize,
            string? category = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            decimal? minRating = null,
            decimal? maxRating = null,
            string? sortBy = null,
            string? searchQuery = null,
            bool ascending = true,
            Guid? storeId = null
        )
        {
            int totalProducts;
            List<Product> products;

            if (storeId.HasValue)
            {
                totalProducts = await _repository.CountAsync(storeId.Value);
                products = await _repository.GetPagedFilteredAndSortedAsync(category, minPrice, maxPrice, minRating, maxRating, sortBy, searchQuery, ascending, page, pageSize, storeId.Value);
            }
            else
            {
                totalProducts = await _repository.CountAsync();
                products = await _repository.GetPagedFilteredAndSortedAsync(category, minPrice, maxPrice, minRating, maxRating, sortBy, searchQuery, ascending, page, pageSize);
            }

            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            return new PagedResult<ProductViewDto>
            {
                Items = products.Select(p => new ProductViewDto
                {
                    Id = p.Id,
                    StoreId = p.StoreId,
                    Name = p.Name,
                    Image = p.Image,
                    Description = p.Description,
                    CategoryId = p.CategoryId,
                    Price = p.Price,
                    Rating = p.Rating,
                    Stock = p.Stock,
                    CreatedAt = p.CreatedAt
                }).ToList(),
                Page = page,
                PageSize = pageSize,
                HasNext = page < totalPages,
                HasPrevious = page > 1 && totalPages >= 1,
                NPages = totalPages,
                NElements = totalProducts
            };
        }
    }
}
