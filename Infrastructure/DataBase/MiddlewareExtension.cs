using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Infrastructure.DataBase
{
    public class MiddlewareExtension
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareExtension> _logger;

        /// <summary>
        /// Initializes a new instance of the MiddlewareExtension class with the specified next delegate and logger.
        /// </summary>
        /// <param name="next">The next delegate in the middleware pipeline.</param>
        /// <param name="logger">The logger used to log information, warnings, and errors.</param>
        public MiddlewareExtension(RequestDelegate next, ILogger<MiddlewareExtension> logger)
        {
            _logger = logger;
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware to handle an incoming HTTP request.
        /// </summary>
        /// <param name="httpContext">The HTTP context for the current request.</param>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        /// <summary>
        /// Handles an exception that occurred during processing of the HTTP request.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <param name="exception">The exception that occurred.</param>
        /// <returns>A task representing the asynchronous handling of the exception.</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var options = new JsonSerializerOptions { WriteIndented = true };
            var errorJson = JsonSerializer.Serialize(exception, options);
            return context.Response.WriteAsync(errorJson);
        }
    }
}
