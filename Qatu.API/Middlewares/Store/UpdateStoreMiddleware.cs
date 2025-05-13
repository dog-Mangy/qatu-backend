using System.Text;
using System.Text.Json;

public class UpdateStoreMiddleware
{
    private readonly RequestDelegate _next;

    public UpdateStoreMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api/stores", StringComparison.OrdinalIgnoreCase) &&
            context.Request.Method.Equals("PUT", StringComparison.OrdinalIgnoreCase))
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();

            context.Request.Body.Position = 0;

            if (string.IsNullOrWhiteSpace(body))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("El cuerpo de la solicitud no puede estar vacío.");
                return;
            }

            try
            {
                var json = JsonSerializer.Deserialize<JsonElement>(body);

                if (!json.TryGetProperty("Name", out var nameProp) || string.IsNullOrWhiteSpace(nameProp.GetString()))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("El campo 'name' es obligatorio y no puede estar vacío.");
                    return;
                }

            }
            catch (JsonException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("El JSON proporcionado no es válido.");
                return;
            }
        }

        await _next(context);
    }
}
