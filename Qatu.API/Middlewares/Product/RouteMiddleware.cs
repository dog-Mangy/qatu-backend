public class RouteMiddleware
{
    private readonly RequestDelegate _next;

    public RouteMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Method != HttpMethods.Get &&
            context.Request.Method != HttpMethods.Delete)
        {
            await _next(context);
            return;
        }

        var pathSegments = context.Request.Path.Value?
            .Split('/', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

        if (pathSegments.Length >= 2 &&
            pathSegments[0] == "api" &&
            pathSegments[1] == "products")
        {

            if (pathSegments.Length == 3)
            {
                if (!Guid.TryParse(pathSegments[2], out var id) || id == Guid.Empty)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("The 'id' parameter must be a valid, non-empty UUID.");
                    return;
                }
            }
        }

        await _next(context);
    }
}
