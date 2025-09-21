using FluentValidation;
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
                _logger.LogError(ex, "Unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            int statusCode;
            object responseBody;

            if (exception is ValidationException validationEx)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                responseBody = new
                {
                    status = statusCode,
                    errors = validationEx.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                };
            }
            else if (exception is KeyNotFoundException notFoundEx)
            {
                statusCode = (int)HttpStatusCode.NotFound;
                responseBody = new
                {
                    status = statusCode,
                    message = notFoundEx.Message
                };
            }
            else if (exception is UnauthorizedAccessException unauthorizedEx)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
                responseBody = new
                {
                    status = statusCode,
                    message = unauthorizedEx.Message
                };
            }
            else
            {
                statusCode = (int)HttpStatusCode.InternalServerError;
                responseBody = new
                {
                    status = statusCode,
                    message = "An unexpected error occurred. Please try again later."
                };
            }

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(responseBody));
        }
    }
}
