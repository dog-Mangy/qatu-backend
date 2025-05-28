using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Net;
using System.Text.Json;

namespace Qatu.API.Middlewares.Sale
{
    public class UpdateSaleMiddleware
    {
        private readonly RequestDelegate _next;

        public UpdateSaleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api/sales") || context.Request.Method != "PUT")
            {
                await _next(context);
                return;
            }

            try
            {
                await _next(context);
            }
            catch (DbUpdateException dbEx) when (dbEx.InnerException is MySqlException mysqlEx)
            {
                context.Response.ContentType = "application/json";
                if (mysqlEx.Number == 1452)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var error = new { error = "Invalid BuyerId, SellerId, ProductId, or ChatId." };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(error));
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var error = new { error = "A database error occurred." };
                    await context.Response.WriteAsync(JsonSerializer.Serialize(error));
                }
            }
            catch (ArgumentException ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                var error = new { error = ex.Message };
                await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var error = new { error = "An unexpected error occurred.", detail = ex.Message };
                await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
        }
    }
}
