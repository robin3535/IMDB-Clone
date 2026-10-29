

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ImdbApi.Filters
{
    public class ResponseWrappingResultFilter : IResultFilter
    {
        private readonly ILogger<ResponseWrappingResultFilter> _logger;

        public ResponseWrappingResultFilter(ILogger<ResponseWrappingResultFilter> logger)
        {
            _logger = logger;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            _logger.LogDebug("Executed result for {ActionName}",
                context.ActionDescriptor.DisplayName);
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _logger.LogDebug("Executing result for {ActionName}",
                context.ActionDescriptor.DisplayName);

            if (context.Result is ObjectResult objectResult
                && objectResult.StatusCode is null or (>= 200 and < 300))
            {
                objectResult.Value = new
                {
                    success = true,
                    data = objectResult.Value,
                    timestamp = DateTime.UtcNow
                };
            }
        }
    }

}