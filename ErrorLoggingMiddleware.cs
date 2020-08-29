using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace HoneywellHackathon
{
    public class ErrorLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorLoggingMiddleware> _logger;
        private Stopwatch _sw;

        public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                _sw = Stopwatch.StartNew();
                await _next.Invoke(context);
                _sw.Stop();
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogDebug(new EventId(), "Authorized Debug Information", ex.StackTrace);
                _logger.LogError(new EventId(), ex.Message, ex);
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized);
            }
            catch (FormatException ex)
            {
                _logger.LogDebug(new EventId(), "Invalid Date Format", ex.StackTrace);
                _logger.LogError(new EventId(), ex.Message, ex);
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogDebug(new EventId(), "EventedEnumerator Debug Information", ex.StackTrace);
                _logger.LogError(new EventId(), ex.Message, ex);
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogDebug("Exception Debug Information", ex.StackTrace);
                _logger.LogError(ex.Message, ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        {
            var response = context.Response;
            
            var statusCode = (int)httpStatusCode;
            var message = "An unhandled exception occurred!";
            _logger.LogInformation((exception.InnerException?.ToString()));
            message = exception.Message;

            response.ContentType = "application/json";
            response.StatusCode = statusCode;

            await response.WriteAsync(JsonConvert.SerializeObject(new CustomErrorResponse
            {
                Message = message
            }));

            var statusMessage = Enum.GetName(typeof(HttpStatusCode), context.Response.StatusCode);
            _logger.LogInformation(
                "HTTP {RequestMethod} {RequestPath} responded {StatusCode} with {StatusMessage} in {Elapsed:0.0000} ms"
                , context.Request.Method
                , context.Request.Path
                , context.Response.StatusCode
                , statusMessage
                , _sw.ElapsedMilliseconds
            );
        }
    }
}