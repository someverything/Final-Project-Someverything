
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace WebAPI.Middlewears
{
    public class GlobalHandlingExeptionsMiddlewear : IMiddleware
    {
        private readonly ILogger<GlobalHandlingExeptionsMiddlewear> _logger;

        public GlobalHandlingExeptionsMiddlewear(ILogger<GlobalHandlingExeptionsMiddlewear> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ArgumentNullException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, "Invalid Argument");
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized, "Unauthorized Access");
            }
            catch (InvalidOperationException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, "Invalid Operation");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                ProblemDetails details = new()
                {
                    Status = (int)HttpStatusCode.InternalServerError,
                    Title = "Server error",
                    Type = "Server error",
                    Detail = ex.Message
                };

                var json = JsonSerializer.Serialize(details);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(json);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode statusCode, string title)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = (int)statusCode;
            ProblemDetails details = new()
            {
                Status = (int)statusCode,
                Title = title,
                Type = title,
                Detail = ex.Message
            };

            var json = JsonSerializer.Serialize(details);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}
