public class ValidateGuidMiddleware
{
    private readonly RequestDelegate _next;

    public ValidateGuidMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var path = context.Request.Path.Value?.ToLower();

        if ((context.Request.Method == HttpMethods.Get ||
            context.Request.Method == HttpMethods.Put ||
            context.Request.Method == HttpMethods.Delete) &&
            path != null && path.StartsWith("/api/stores"))
        {
            var segments = path.Split("/", StringSplitOptions.RemoveEmptyEntries);

            if (segments.Length == 2)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("The ID is missing from the route.");
                return;
            }

            var idSegment = segments.Last();
            if (!Guid.TryParse(idSegment, out _))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("The provided ID is not valid.");
                return;
            }
        }

        await _next(context);
    }

}
