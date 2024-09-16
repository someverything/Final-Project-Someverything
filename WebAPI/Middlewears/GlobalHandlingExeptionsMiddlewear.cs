using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
            catch (ArgumentOutOfRangeException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, "Argument Out of Range");
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, "Invalid Argument Provided");
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized, "Unauthorized Access");
            }
            catch (InvalidOperationException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, "Invalid Operation");
            }
            catch (NullReferenceException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "Null Reference Occurred");
            }
            catch (IndexOutOfRangeException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest, "Index Out of Range");
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotFound, "Key Not Found");
            }
            catch (NotImplementedException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.NotImplemented, "Feature Not Implemented");
            }
            catch (TimeoutException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.RequestTimeout, "Operation Timed Out");
            }
            catch (IOException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "IO Exception Occurred");
            }
            catch (SqlException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError, "Database Error Occurred");
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
