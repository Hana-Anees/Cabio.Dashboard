using System.Net;
using System.Text.Json;

namespace Cabio.Dashboard.Api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message = exception.Message;

            // Determine HTTP status code based on exception type
            switch (exception)
            {
                case UnauthorizedAccessException:
                    status = HttpStatusCode.Unauthorized; // 401
                    break;

                case ArgumentException:
                case InvalidOperationException:
                    status = HttpStatusCode.BadRequest; // 400
                    break;

                case KeyNotFoundException:
                    status = HttpStatusCode.NotFound; // 404
                    break;

                default:
                    status = HttpStatusCode.InternalServerError; // 500
                    break;
            }

            var response = new
            {
                success = false,
                status = (int)status,
                error = message,
                timestamp = DateTime.UtcNow
            };

            var payload = JsonSerializer.Serialize(response);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(payload);
        }
    }
}
