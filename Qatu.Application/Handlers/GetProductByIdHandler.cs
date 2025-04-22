using Qatu.Domain.Entities;
using Qatu.Domain.Interfaces;

public class GetProductByIdHandler
{
    private readonly IProductRepository _repository;

    public GetProductByIdHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<Product?> HandleAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }
}
