using Qatu.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qatu.Domain.Interfaces;
using Qatu.Application.DTOs.Product;
using Qatu.Domain.Entities;

namespace Qatu.Application.UseCases.Products
{
    public class GetProductsPagedUseCase
    {
        private readonly IProductRepository _repository;

        public GetProductsPagedUseCase(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<PagedResult<ProductViewDto>> ExecuteAsync(int page, int pageSize, int? storeId = null)
        {
            int totalProducts;
            List<Product> products;

            if (storeId.HasValue)
            {
                totalProducts = await _repository.CountByStoreAsync(storeId.Value);
                products = await _repository.GetPagedByStoreAsync(storeId.Value, page, pageSize);
            }
            else
            {
                totalProducts = await _repository.CountAsync();
                products = await _repository.GetPagedAsync(page, pageSize);
            }

            var totalPages = (int) Math.Ceiling((double)totalProducts / pageSize);

            return new PagedResult<ProductViewDto>
            {
                Items = products.Select(p => new ProductViewDto
                {
                    Id = p.Id,
                    StoreId = p.StoreId,
                    Name = p.Name,
                    Description = p.Description,
                    Category = p.Category,
                    Price = p.Price,
                    Rating = p.Rating,
                    Stock = p.Stock,
                    CreatedAt = p.CreatedAt
                }).ToList(),
                Page = page,
                PageSize = pageSize,
                HasNext = page < totalPages,
                HasPrevious = page>1 && totalPages>1,
                NPages = totalPages,
                NElements = totalProducts
            };
        }
    }
}
