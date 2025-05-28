using System.Security.Claims;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Qatu.Application.UseCases.Categories;
using Qatu.Application.UseCases.Chat;
using Qatu.Application.UseCases.Products;
using Qatu.Application.UseCases.Stores;
using Qatu.Domain.Interfaces;
using Qatu.Infrastructure.Persistence;
using Qatu.Infrastructure.Persistence.Repositories;
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
        policy.RequireClaim($"{audience}/roles", "Admin"));

    options.AddPolicy("VendorPolicy", policy =>
        policy.RequireClaim($"{audience}/roles", "Vendor"));

    options.AddPolicy("UserPolicy", policy =>
        policy.RequireClaim($"{audience}/roles", "User"));
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

//Category
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CreateCategoryUseCase>();
builder.Services.AddScoped<GetCategoryByIdUseCase>();
builder.Services.AddScoped<UpdateCategoryUseCase>();
builder.Services.AddScoped<DeleteCategoryUseCase>();
builder.Services.AddScoped<GetAllCategoriesUseCase>();

//Chats
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<GetChatsByUserIdUseCase>();
builder.Services.AddScoped<CreateChatUseCase>();

//Message
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<GetMessagesByChatIdUseCase>();
builder.Services.AddScoped<CreateMessageUseCase>();

//Sale
builder.Services.AddScoped<ISaleRepository, SaleRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultDevConnection");

builder.Services.AddDbContext<QatuDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


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

app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();



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

//Category

app.MapControllers();

app.Run();
