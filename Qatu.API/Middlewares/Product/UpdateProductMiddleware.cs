using System.Net;

using Microsoft.EntityFrameworkCore;

using MySqlConnector;

public class UpdateProductMiddleware
{
    private readonly RequestDelegate _next;

    public UpdateProductMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DbUpdateException dbEx) when (dbEx.InnerException is MySqlException mysqlEx)
        {
            if (mysqlEx.Number == 1452)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";
                var error = new { error = "StoreId or UserId does not exist or is invalid." };
                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(error));
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var error = new { error = "A database error occurred." };
                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(error));
            }
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
            var error = new { error = "An unexpected error occurred.", detail = ex.Message };
            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(error));
        }
    }
}
