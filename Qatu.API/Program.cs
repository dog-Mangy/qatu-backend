using Microsoft.EntityFrameworkCore;
using Qatu.Infrastructure.Persistence;
using Qatu.Domain.Interfaces;
using Qatu.Infrastructure.Repositories;
using Qatu.Application.UseCases.Products;
using Qatu.Application.UseCases.Stores;

var builder = WebApplication.CreateBuilder(args);

//Product
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<GetProductByIdUseCase>();
builder.Services.AddScoped<UpdateProductUseCase>();
builder.Services.AddScoped<UpdateProductPriceUseCase>();
builder.Services.AddScoped<UpdateProductStockUseCase>();
builder.Services.AddScoped<DeleteProductUseCase>();
builder.Services.AddScoped<CreateProductUseCase>();
builder.Services.AddScoped<CreateProductListUseCase>();
builder.Services.AddScoped<GetProductsUseCase>();

//Store
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<CreateStoreUseCase>();
builder.Services.AddScoped<GetStoreByIdUseCase>();
builder.Services.AddScoped<UpdateStoreUseCase>();
builder.Services.AddScoped<DeleteStoreUseCase>();




var connectionString = builder.Configuration.GetConnectionString("DefaultDevConnection");

builder.Services.AddDbContext<QatuDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();

    Console.WriteLine("Swagger UI is available at: http://localhost:5028/swagger/index.html");
}

app.UseHttpsRedirection();

app.UseSwaggerUI(o => o.SwaggerEndpoint("/openapi/v1.json", "Swagger Demo"));

// Product
app.UseMiddleware<RouteMiddleware>();
app.UseMiddleware<CreateProductMiddleware>();
app.UseMiddleware<NewPriceMiddleware>();
app.UseMiddleware<NewStockMiddleware>();
app.UseMiddleware<ProductListMiddleware>();
app.UseMiddleware<PaginationMiddleware>();
app.UseMiddleware<UpdateProductMiddleware>();

// Store
app.UseMiddleware<ValidateGuidMiddleware>();
app.UseMiddleware<CreateStoreMiddleware>();
app.UseMiddleware<UpdateStoreMiddleware>();




app.MapControllers();

app.Run();
