using System;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

namespace Qatu.API.Middlewares.Chat
{
    public class UserValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public UserValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api/chats") &&
                context.Request.Method == "GET" &&
                !(context.Request.Path.Value?.Contains("/messages") ?? false))
            {
                var userIdString = context.Request.Query["userId"].ToString();
                if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId) || userId == Guid.Empty)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Valid User ID is required.");
                    return;
                }
            }

            await _next(context);
        }
    }
}
