using Qatu.Domain.Interfaces;


public class UpdateProductStockHandler
{
    private readonly IProductRepository _repository;

    public UpdateProductStockHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> HandleAsync(UpdateProductStockDto command)
    {
        var product = await _repository.GetByIdAsync(command.ProductId);
        if (product == null)
            return false;

        product.Stock = command.NewStock;
        await _repository.UpdateAsync(product);
        return true;
    }
}
