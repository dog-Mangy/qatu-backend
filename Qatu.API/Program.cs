using Microsoft.EntityFrameworkCore;
using Qatu.Infrastructure.Persistence;
using Qatu.Domain.Interfaces;
using Qatu.Infrastructure.Repositories;
using Qatu.Application.UseCases.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GetProductByIdUseCase>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<UpdateProductPriceUseCase>();
builder.Services.AddScoped<UpdateProductStockUseCase>();
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<CreateProductListUseCase>();

var connectionString = builder.Configuration.GetConnectionString("DefaultDevConnection");

builder.Services.AddDbContext<QatuDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
