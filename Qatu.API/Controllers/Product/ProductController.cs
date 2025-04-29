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
    private readonly GetProductsByStoreIdUseCase _getProductsByStoreId;
    private readonly DeleteProductUseCase _deleteProduct;
    private readonly GetProductsPagedUseCase _getProductsPaged;




    public ProductsController(GetProductByIdUseCase getProductById,
                              UpdateProductPriceUseCase updatePrice,
                              UpdateProductStockUseCase updateStock,
                              CreateProductUseCase createProduct,
                              CreateProductListUseCase createProductList,
                              GetProductsByStoreIdUseCase getProductsByStoreId,
                              DeleteProductUseCase deleteProduct,
                              GetProductsPagedUseCase getProducstPaged)
    {
        _getProductById = getProductById;
        _updatePrice = updatePrice;
        _updateStock = updateStock;
        _createProduct = createProduct;
        _createProductList = createProductList;
        _getProductsByStoreId = getProductsByStoreId;
        _deleteProduct = deleteProduct;
        _getProductsPaged = getProducstPaged;
    }
    [HttpGet]
    public async Task<IActionResult> GetPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _getProductsPaged.ExecuteAsync(page, pageSize);
        return Ok(result);
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

    [HttpPost("bulk")]
    public async Task<IActionResult> CreateMultipleProducts([FromBody] CreateProductListDto dto)
    {
        var multipleProducts = await _createProductList.HandleAsync(dto.Products);

        return Ok(multipleProducts);
    }


    [HttpGet("/api/stores/{storeId}/products")]
    public async Task<IActionResult> GetByStoreId(int storeId, [FromQuery] int? page, [FromQuery] int? pageSize)
    {
        var result = await _getProductsPaged.ExecuteAsync(page.Value, pageSize.Value, storeId);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _deleteProduct.ExecuteAsync(id);
        if (!result) return NotFound();

        return NoContent(); 
    }
}