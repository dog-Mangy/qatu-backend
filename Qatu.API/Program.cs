using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Qatu.Infrastructure.Persistence;
using Qatu.Domain.Interfaces;
using Qatu.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GetProductByIdHandler>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

var connectionString = builder.Configuration.GetConnectionString("DefaultDevConnection");

builder.Services.AddDbContext<QatuDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);

builder.Services.AddControllers(); 

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers(); 

app.Run();
