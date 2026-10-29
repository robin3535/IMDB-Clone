using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ImdbApi.Common
{
    public class RequestLoggingMiddleware
    {
        private readonly ILogger<RequestLoggingMiddleware> _logger;
        private readonly RequestDelegate _next;

        /// <summary>
        /// Custom MiddleWare contructor
        /// </summary>
        /// <param name="next">A request delegate is a function that processes an HTTP request.</param>
        /// <param name="logger">Logging</param>
        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }

        /// <summary>
        /// InvokeAsync method that takes HttpContext and returns Task
        /// </summary>
        /// <param name="context">HttpContext is the object that carries everything about the current HTTP request and response.</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            // Before - Request flow from client to server
            var startTime = DateTime.UtcNow;
            _logger.LogInformation("Incoming {Method} {Path}", context.Request.Method, context.Request.Path);

            await _next(context);

            // After - Request flow from server to client
            var elapsed = DateTime.UtcNow - startTime;
            _logger.LogInformation("Completed {Method} {Path} with {StatusCode} in {Elapsed}ms",
                context.Request.Method, context.Request.Path, context.Response.StatusCode, elapsed.TotalMilliseconds);
        }
    }

    public static class RequestLoggingExtension
    {
        public static IApplicationBuilder UseRequestLoggingMiddleWare(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestLoggingMiddleware>();
        }
    }
}