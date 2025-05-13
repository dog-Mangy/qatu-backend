using System.Text;
using System.Text.Json;

public class ProductListMiddleware
{
    private readonly RequestDelegate _next;

    public ProductListMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/api/products/bulk") &&
            context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(
                context.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true
            );

            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            if (string.IsNullOrWhiteSpace(body))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("The request body cannot be empty.");
                return;
            }

            try
            {
                var json = JsonDocument.Parse(body);
                if (!json.RootElement.TryGetProperty("products", out var products) || products.ValueKind != JsonValueKind.Array || products.GetArrayLength() == 0)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("You must include at least one product in the 'products' property.");
                    return;
                }
            }
            catch (JsonException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("The request body does not contain valid JSON.");
                return;
            }
        }

        await _next(context);
    }
}
