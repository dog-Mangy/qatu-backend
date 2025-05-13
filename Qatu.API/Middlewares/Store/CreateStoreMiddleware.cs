using System.Text;
using System.Text.Json;

public class CreateStoreMiddleware
{
    private readonly RequestDelegate _next;

    public CreateStoreMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api/stores", StringComparison.OrdinalIgnoreCase) &&
            context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();

            context.Request.Body.Position = 0;

            if (string.IsNullOrWhiteSpace(body))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("the request body is empty");
                return;
            }

            try
            {
                var json = JsonSerializer.Deserialize<JsonElement>(body);

                if (!json.TryGetProperty("UserId", out var userIdProp) || userIdProp.GetGuid() == Guid.Empty)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("The 'UserId' field is required and must have a valid GUID.");
                    return;
                }

                if (!json.TryGetProperty("Name", out var nameProp) || string.IsNullOrWhiteSpace(nameProp.GetString()))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("The 'Name' field is required");
                    return;
                }

            }
            catch (JsonException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("El JSON no es válido.");
                return;
            }
        }

        await _next(context);
    }
}