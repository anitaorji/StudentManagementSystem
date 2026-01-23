using System;
using System.Net;
using System.Text.Json;

namespace StudentManagement.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                await HandleExceptionAsync(context, e);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var message = "An unexpected error occured.";

            if (e is KeyNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                message = e.Message;
            }

            if (e is ArgumentException)
            {
                statusCode = HttpStatusCode.BadRequest;
                message = e.Message;
            }

            var response = new
            {
                status = (int)statusCode,
                error = message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
