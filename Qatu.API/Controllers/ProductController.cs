// Controllers/ProductController.cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly GetProductByIdHandler _handler;

    public ProductsController(GetProductByIdHandler handler)
    {
        _handler = handler;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _handler.HandleAsync(id);
        if (product == null)
            return NotFound();

        return Ok(product);
    }
}
