using System.Net;
using System.Text.Json;

using Microsoft.EntityFrameworkCore;

using MySqlConnector;

namespace Qatu.API.Middlewares.Sale
{
    public class CheckSaleRelationshipMiddleware
    {
        private readonly RequestDelegate _next;

        public CheckSaleRelationshipMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Path.StartsWithSegments("/api/sales/check-relationship") || context.Request.Method != "GET")
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
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new { error = "A database error occurred." };
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
