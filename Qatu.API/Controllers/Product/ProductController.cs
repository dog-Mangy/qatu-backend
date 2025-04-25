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
    private readonly GetProductsByStoreIdUseCase _getProductsByStoreId;
    private readonly DeleteProductUseCase _deleteProduct;


    public ProductsController(GetProductByIdUseCase getProductById,
                              UpdateProductPriceUseCase updatePrice,
                              UpdateProductStockUseCase updateStock,
                              GetProductsByStoreIdUseCase getProductsByStoreId,
                              DeleteProductUseCase deleteProduct)
    {
        _getProductById = getProductById;
        _updatePrice = updatePrice;
        _updateStock = updateStock;
        _getProductsByStoreId = getProductsByStoreId;
        _deleteProduct = deleteProduct;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _getProductById.ExecuteAsync(id);
        if (product == null) return NotFound();
        return Ok(product);
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

    [HttpGet("store/{storeId}")]
    public async Task<IActionResult> GetByStoreId(int storeId)
    {
        var products = await _getProductsByStoreId.ExecuteAsync(storeId);
        return Ok(products);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deleteProduct.ExecuteAsync(id);
        if (!result) return NotFound();

        return NoContent(); 
    }
}