using System.Text;
using System.Text.Json;

public class NewStockMiddleware
{
    private readonly RequestDelegate _next;

    public NewStockMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Put &&
            context.Request.Path.Equals("/api/products/update-stock", StringComparison.OrdinalIgnoreCase))
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            if (string.IsNullOrWhiteSpace(body))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Empty JSON body.");
                return;
            }

            JsonDocument jsonDoc;
            try
            {
                jsonDoc = JsonDocument.Parse(body);
            }
            catch (JsonException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid JSON format.");
                return;
            }

            using (jsonDoc)
            {
                var root = jsonDoc.RootElement;

                if (!root.TryGetProperty("newStock", out var stockProp) ||
                    stockProp.ValueKind != JsonValueKind.Number)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Missing or invalid 'newStock' field in the request body.");
                    return;
                }

                var stock = stockProp.GetInt32();
                if (stock <= 0)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("'newStock' must be greater than 0.");
                    return;
                }
            }
        }

        await _next(context);
    }
}
