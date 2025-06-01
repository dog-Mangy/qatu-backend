using System.Security.Claims;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using Qatu.API.Middlewares.Sale;
using Qatu.Application.UseCases.Categories;
using Qatu.Application.UseCases.Chat;
using Qatu.Application.UseCases.Products;
using Qatu.Application.UseCases.Sale;
using Qatu.Application.UseCases.Stores;
using Qatu.Application.UseCases.Requests;
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
    options.AddPolicy("UserPolicy", policy =>
        policy.RequireAssertion(context =>
        {
            var roles = context.User.FindAll($"{audience}/roles").Select(c => c.Value);
            return roles.Contains("User") || roles.Contains("Vendor") || roles.Contains("Admin");
        }));

    options.AddPolicy("VendorPolicy", policy =>
        policy.RequireAssertion(context =>
        {
            var roles = context.User.FindAll($"{audience}/roles").Select(c => c.Value);
            return roles.Contains("Vendor") || roles.Contains("Admin");
        }));

    options.AddPolicy("AdminPolicy", policy =>
        policy.RequireClaim($"{audience}/roles", "Admin"));
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
builder.Services.AddScoped<GetStoresUseCase>();
builder.Services.AddScoped<GetStoresByUserIdUseCase>();
builder.Services.AddScoped<UpdateStoreUseCase>();
builder.Services.AddScoped<DeleteStoreUseCase>();


//Category
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<CreateCategoryUseCase>();
builder.Services.AddScoped<GetCategoryByIdUseCase>();
builder.Services.AddScoped<UpdateCategoryUseCase>();
builder.Services.AddScoped<DeleteCategoryUseCase>();
builder.Services.AddScoped<GetAllCategoriesUseCase>();

//Requests
builder.Services.AddScoped<IRequestRepository, RequestRepository>();
builder.Services.AddScoped<GetAllRequestsUseCase>();
builder.Services.AddScoped<CreateRequestUseCase>();
builder.Services.AddScoped<DeleteRequestUseCase>();
builder.Services.AddScoped<UpdateRequestStatusUseCase>();


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
builder.Services.AddScoped<UpdateSaleUseCase>();
builder.Services.AddScoped<GetSaleByIdUseCase>();
builder.Services.AddScoped<GetSaleByChatIdUseCase>();
builder.Services.AddScoped<CheckSaleRelationshipUseCase>();

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
app.UseCors("AllowFrontend");


app.UseAuthentication();
app.UseAuthorization();

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
app.UseMiddleware<CreateStoreMiddleware>();
app.UseMiddleware<UpdateStoreMiddleware>();

//Category
// Sale
app.UseMiddleware<UpdateSaleMiddleware>();
app.UseMiddleware<GetSaleMiddleware>();
app.UseMiddleware<CheckSaleRelationshipMiddleware>();

app.MapControllers();

app.Run();
