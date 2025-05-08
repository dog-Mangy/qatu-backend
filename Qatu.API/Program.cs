using Microsoft.EntityFrameworkCore;
using Qatu.Infrastructure.Persistence;
using Qatu.Domain.Interfaces;
using Qatu.Infrastructure.Repositories;
using Qatu.Application.UseCases.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GetProductByIdUseCase>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<UpdateProductUseCase>();
builder.Services.AddScoped<UpdateProductPriceUseCase>();
builder.Services.AddScoped<UpdateProductStockUseCase>();
builder.Services.AddScoped<GetProductsByStoreIdUseCase>();
builder.Services.AddScoped<DeleteProductUseCase>();
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<CreateProductListUseCase>();
builder.Services.AddScoped<GetProductsPagedUseCase>();


var connectionString = builder.Configuration.GetConnectionString("DefaultDevConnection");

builder.Services.AddDbContext<QatuDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseMiddleware<RouteMiddleware>();
app.UseMiddleware<CreateProductMiddleware>();
app.UseMiddleware<NewPriceMiddleware>();
app.UseMiddleware<NewStockMiddleware>();
app.UseMiddleware<ProductListMiddleware>();
app.UseMiddleware<PaginationMiddleware>();
app.UseMiddleware<UpdateProductMiddleware>();


app.MapControllers();

app.Run();
