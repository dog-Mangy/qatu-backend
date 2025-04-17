using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Qatu.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultDevConnection");

builder.Services.AddDbContext<QatuDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
);


var app = builder.Build();

app.UseHttpsRedirection();

app.Run();