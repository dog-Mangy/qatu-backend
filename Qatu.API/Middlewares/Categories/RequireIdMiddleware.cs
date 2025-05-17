public class RequireIdMiddleware
{
    private readonly RequestDelegate _next;

    public RequireIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var method = context.Request.Method;
        var path = context.Request.Path.ToString();

        if ((method == HttpMethods.Get || method == HttpMethods.Put || method == HttpMethods.Delete)
            && path.Contains("/api/categories", StringComparison.OrdinalIgnoreCase)
            && !Guid.TryParse(path.Split('/').Last(), out _))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync("Category ID is required in the route.");
            return;
        }

        await _next(context);
    }
}
