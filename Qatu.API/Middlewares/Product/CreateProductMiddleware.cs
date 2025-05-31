using System.Text;
using System.Text.Json;
 
public class CreateProductMiddleware
{
    private readonly RequestDelegate _next;
 
    public CreateProductMiddleware(RequestDelegate next)
    {
        _next = next;
    }
 
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.Equals("/api/products", StringComparison.OrdinalIgnoreCase) &&
            context.Request.Method.Equals("POST", StringComparison.OrdinalIgnoreCase))
        {
            context.Request.EnableBuffering();
            using var reader = new StreamReader(
                context.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 1024,
                leaveOpen: true);
 
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
 
            var errors = new List<string>();
 
            try
            {
                var jsonDoc = JsonDocument.Parse(body);
                var root = jsonDoc.RootElement;
 
                if (!root.TryGetProperty("storeId", out var storeIdProp) ||
                    !Guid.TryParse(storeIdProp.GetString(), out var storeId) || storeId == Guid.Empty)
                    errors.Add("StoreId must be a valid non-empty UUID.");
 
                if (!root.TryGetProperty("price", out var priceProp) ||
                    !priceProp.TryGetDecimal(out var price) || price < 0)
                    errors.Add("Price must be a non-negative decimal number.");
 
                if (!root.TryGetProperty("stock", out var stockProp) ||
                    !stockProp.TryGetInt32(out var stock) || stock < 0)
                    errors.Add("Stock must be a non-negative integer.");
            }
            catch (JsonException)
            {
                errors.Add("Invalid JSON format.");
            }
 
            if (errors.Any())
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(new { errors }));
                return;
            }
        }
 
        await _next(context);
    }
}