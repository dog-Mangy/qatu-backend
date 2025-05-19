using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Qatu.Application.UseCases.Products;
using Qatu.Application.UseCases.Stores;
using Qatu.Domain.Interfaces;
using Qatu.Infrastructure.Persistence;
using Qatu.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

var domain = builder.Configuration["Auth0:Domain"];
var audience = builder.Configuration["Auth0:Audience"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.Authority = $"https://{domain}/";
    options.Audience = audience;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = $"https://{domain}/",
        ValidAudience = audience
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireClaim("https://qatu.api/roles", "Admin"));

    options.AddPolicy("VendorPolicy", policy =>
        policy.RequireClaim("https://qatu.api/roles", "Vendor"));

    options.AddPolicy("UserPolicy", policy =>
        policy.RequireClaim("https://qatu.api/roles", "User"));
});


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

app.UseAuthentication();
app.UseAuthorization();

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
