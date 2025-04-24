using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly GetProductByIdHandler _handler;
    private readonly UpdateProductPriceHandler _updateHandler;


    public ProductsController(GetProductByIdHandler handler, UpdateProductPriceHandler updateHandler)
    {
        _handler = handler;
        _updateHandler = updateHandler;

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
}


