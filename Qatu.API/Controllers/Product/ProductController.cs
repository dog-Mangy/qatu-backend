using System.Globalization;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Qatu.Application.DTOs.Product;
using Qatu.Application.UseCases.Products;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly GetProductByIdUseCase _getProductById;
    private readonly UpdateProductPriceUseCase _updatePrice;
    private readonly UpdateProductStockUseCase _updateStock;
    private readonly CreateProductUseCase _createProduct;
    private readonly CreateProductListUseCase _createProductList;
    private readonly UpdateProductUseCase _updateProduct;
    private readonly DeleteProductUseCase _deleteProduct;
    private readonly GetProductsUseCase _getProducts;

    public ProductsController(GetProductByIdUseCase getProductById,
                              UpdateProductPriceUseCase updatePrice,
                              UpdateProductStockUseCase updateStock,
                              UpdateProductUseCase updateProduct,
                              CreateProductUseCase createProduct,
                              CreateProductListUseCase createProductList,
                              DeleteProductUseCase deleteProduct,
                              GetProductsUseCase getProducst
                              )
    {
        _getProductById = getProductById;
        _updatePrice = updatePrice;
        _updateStock = updateStock;
        _updateProduct = updateProduct;
        _createProduct = createProduct;
        _createProductList = createProductList;
        _deleteProduct = deleteProduct;
        _getProducts = getProducst;
    }
    [HttpGet]
    public async Task<IActionResult> GetProducts(
            [FromQuery] string? category,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] decimal? minRating,
            [FromQuery] decimal? maxRating,
            [FromQuery] string? searchQuery,
            [FromQuery] string? sortBy = "CreatedAt",
            [FromQuery] bool ascending = true,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
    {
        var result = await _getProducts.ExecuteAsync(
            page, pageSize, category, minPrice, maxPrice, minRating, maxRating, sortBy, searchQuery, ascending
        );
        return Ok(result);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _getProductById.ExecuteAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "VendorPolicy")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductDto dto)
    {
        var product = await _getProductById.ExecuteAsync(id);

        if (product == null) return NotFound();

        var userId = User.Claims.FirstOrDefault(c => c.Type == "https://qatu.api/uuid")?.Value;

        var ownerID = product.Store.User.Id.ToString();

        if (ownerID != userId)
        {
            return Forbid("You are not the owner of the product");
        }

        var result = await _updateProduct.ExecuteAsync(id, dto);
        return result ? NoContent() : NotFound();
    }

    [HttpPut("update-price")]
    public async Task<IActionResult> UpdatePrice([FromBody] UpdateProductPriceDto dto)
    {
        var result = await _updatePrice.ExecuteAsync(dto);
        return result ? NoContent() : NotFound();
    }

    [HttpPut("update-stock")]
    public async Task<IActionResult> UpdateStock([FromBody] UpdateProductStockDto dto)
    {
        var result = await _updateStock.ExecuteAsync(dto);
        return result ? NoContent() : NotFound();
    }

    [HttpPost]

    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto dto)
    {
        var product = await _createProduct.HandleAsync(dto);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPost("bulk")]
    public async Task<IActionResult> CreateMultipleProducts([FromBody] CreateProductListDto dto)
    {
        var multipleProducts = await _createProductList.HandleAsync(dto.Products);

        return Ok(multipleProducts);
    }


    [HttpGet("/api/stores/{storeId}/products")]
    public async Task<IActionResult> GetByStoreId(
            Guid storeId,
            [FromQuery] string? category,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] decimal? minRating,
            [FromQuery] decimal? maxRating,
            [FromQuery] string? searchQuery,
            [FromQuery] string? sortBy = "CreatedAt",
            [FromQuery] bool ascending = true,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
    {
        var result = await _getProducts.ExecuteAsync(
            page, pageSize, category, minPrice, maxPrice, minRating, maxRating, sortBy, searchQuery, ascending, storeId
        );
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _deleteProduct.ExecuteAsync(id);
        if (!result) return NotFound();

        return NoContent();
    }
}
