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

    public ProductsController(GetProductByIdUseCase getProductById,
                              UpdateProductPriceUseCase updatePrice,
                              UpdateProductStockUseCase updateStock,
                              CreateProductUseCase createProduct)
    {
        _getProductById = getProductById;
        _updatePrice = updatePrice;
        _updateStock = updateStock;
        _createProduct = createProduct;
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

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto command)
    {
        var product = await _createProduct.HandleAsync(command);

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }
}
