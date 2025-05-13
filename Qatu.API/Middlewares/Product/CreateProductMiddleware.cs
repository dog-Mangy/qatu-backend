using System.Text;
using System.Text.RegularExpressions;

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

            var storeIdMatch = Regex.Match(body, @"""storeId""\s*:\s*""([^""]+)""", RegexOptions.IgnoreCase);
            var priceMatch = Regex.Match(body, @"""price""\s*:\s*(\S+)", RegexOptions.IgnoreCase);
            var stockMatch = Regex.Match(body, @"""stock""\s*:\s*(\S+)", RegexOptions.IgnoreCase);

            if (storeIdMatch.Success)
            {
                var storeIdRaw = storeIdMatch.Groups[1].Value;

                if (!Guid.TryParse(storeIdRaw, out var storeId) || storeId == Guid.Empty)
                    errors.Add("StoreId must be a valid non-empty UUID.");
            }
            else
            {
                errors.Add("StoreId is required and must be a valid UUID.");
            }

            if (priceMatch.Success)
            {
                if (!decimal.TryParse(priceMatch.Groups[1].Value, out var price) || price < 0)
                    errors.Add("Price must be a non-negative decimal number.");
            }
            else
            {
                errors.Add("Price is required and must be valid.");
            }

            if (stockMatch.Success)
            {
                if (!int.TryParse(stockMatch.Groups[1].Value, out var stock) || stock < 0)
                    errors.Add("Stock must be a non-negative integer.");
            }
            else
            {
                errors.Add("Stock is required and must be valid.");
            }

            if (errors.Any())
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(new { errors }));
                return;
            }
        }

        await _next(context);
    }
}
