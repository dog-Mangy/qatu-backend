using Qatu.Domain.Interfaces;


public class UpdateProductPriceHandler
{
    private readonly IProductRepository _repository;

    public UpdateProductPriceHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<bool> HandleAsync(UpdateProductPriceDto command)
    {
        var product = await _repository.GetByIdAsync(command.ProductId);
        if (product == null)
            return false;

        product.Price = command.NewPrice;
        await _repository.UpdateAsync(product);
        return true;
    }
}
