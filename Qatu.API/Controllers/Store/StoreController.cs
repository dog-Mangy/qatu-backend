using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Qatu.Application.DTOs.Store;
using Qatu.Application.UseCases.Stores;

[ApiController]
[Route("api/[controller]")]
public class StoresController : ControllerBase
{
    private readonly CreateStoreUseCase _createStore;
    private readonly GetStoreByIdUseCase _getStoreById;
    private readonly UpdateStoreUseCase _updateStore;
    private readonly DeleteStoreUseCase _deleteStore;
    private readonly GetStoresUseCase _getStores;

    public StoresController(
        CreateStoreUseCase createStore,
        GetStoreByIdUseCase getStoreById,
        GetStoresUseCase getStores,
        UpdateStoreUseCase updateStore,
        DeleteStoreUseCase deleteStore)
    {
        _createStore = createStore;
        _getStoreById = getStoreById;
        _getStores = getStores;
        _updateStore = updateStore;
        _deleteStore = deleteStore;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var store = await _getStoreById.ExecuteAsync(id);
        if (store == null)
            return NotFound();

        return Ok(store);
    }

    [HttpGet]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> GetAll()
    {
        var stores = await _getStores.ExecuteAsync();
        return Ok(stores);
    }

    [HttpPost]
    [Authorize(Policy = "AdminPolicy")]
    public async Task<IActionResult> Create([FromBody] CreateStoreDto dto)
    {
        var createdStore = await _createStore.HandleAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdStore.Id }, createdStore);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateStoreDto dto)
    {
        var updatedStore = await _updateStore.HandleAsync(id, dto);
        if (updatedStore == null)
            return NotFound();

        return Ok(updatedStore);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleted = await _deleteStore.ExecuteAsync(id);
        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
