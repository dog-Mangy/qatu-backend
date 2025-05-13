public class PaginationMiddleware
{
    private readonly RequestDelegate _next;

    public PaginationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method == HttpMethods.Get &&
            context.Request.Path.StartsWithSegments("/api/stores", out var remainingPath) &&
            remainingPath.HasValue && remainingPath.Value.Contains("/products"))
        {
            var query = context.Request.Query;

            if (query.TryGetValue("page", out var pageStr) &&
                int.TryParse(pageStr, out int page) &&
                page < 1)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Page must be greater than or equal to 1.");
                return;
            }

            if (query.TryGetValue("pageSize", out var sizeStr) &&
                int.TryParse(sizeStr, out int pageSize) &&
                (pageSize < 1 || pageSize > 100))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("PageSize must be between 1 and 100.");
                return;
            }
        }

        await _next(context);
    }
}
