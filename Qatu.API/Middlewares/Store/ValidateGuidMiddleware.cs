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

            if (context.Request.Method == HttpMethods.Get && segments.Length == 2)
            {
                await _next(context);
                return;
            }

            if (segments.Length == 3)
            {
                var idSegment = segments[2];
                if (!Guid.TryParse(idSegment, out _))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("The provided ID is not a valid GUID.");
                    return;
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("The ID is missing or the route format is incorrect.");
                return;
            }
        }

        await _next(context);
    }
}
