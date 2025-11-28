using System.Net;
using System.Text.Json;

namespace MovieAppApi.Src.Core.Middlewares;

public class ExceptionHandlingMiddleware
{
  private readonly RequestDelegate _next;
  private readonly ILogger<ExceptionHandlingMiddleware> _logger;

  public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
      _logger.LogError(ex, "An unhandled exception occurred.");
      await HandleExceptionAsync(context, ex);
    }
  }

  private Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    var isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

    if (isDevelopment)
    {
      return WriteDevelopmentResponse(context, exception);
    }
    else
    {
      return WriteProductionResponse(context);
    }
  }

  private Task WriteDevelopmentResponse(HttpContext context, Exception exception)
  {
    var payload = new
    {
      StatusCode = (int)HttpStatusCode.InternalServerError,
      Message = "An unexpected error occurred.",
      Details = exception.Message,
      Trace = exception.StackTrace?.Split(Environment.NewLine)
    };

    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

    return context.Response.WriteAsync(JsonSerializer.Serialize(payload));
  }

  private Task WriteProductionResponse(HttpContext context)
  {
    context.Response.ContentType = "text/plain";
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

    return context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
  }
}
