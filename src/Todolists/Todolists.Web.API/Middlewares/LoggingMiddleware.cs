namespace Todolists.Web.API.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next,
        ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        finally
        {
            _logger.LogInformation("Request: {method} {url}\nResponse: {statusCode}\n{body}",
                httpContext.Request?.Method,
                httpContext.Request?.Path.Value,
                httpContext.Response?.StatusCode,
                httpContext.Response?.Body);
        }
    }
}

public static class LoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        => builder.UseMiddleware<LoggingMiddleware>();
}