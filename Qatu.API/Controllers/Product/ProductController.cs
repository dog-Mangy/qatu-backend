using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly GetProductByIdHandler _handler;
    private readonly UpdateProductPriceHandler _updateHandler;

    private readonly UpdateProductStockHandler _updateStockHandler;


    public ProductsController(GetProductByIdHandler handler, UpdateProductPriceHandler updateHandler, UpdateProductStockHandler updateStockHandler)
    {
        _handler = handler;
        _updateHandler = updateHandler;
        _updateStockHandler = updateStockHandler;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _handler.HandleAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpPut("update-price")]
    public async Task<IActionResult> UpdatePrice([FromBody] UpdateProductPriceDto command)
    {
        var result = await _updateHandler.HandleAsync(command);
        if (!result)
            return NotFound();

        return NoContent();
    }

    [HttpPut("update-stock")]
    public async Task<IActionResult> UpdateStock([FromBody] UpdateProductStockDto command)
    {
        var result = await _updateStockHandler.HandleAsync(command);
        if (!result)
            return NotFound();

        return NoContent();
    }
}


