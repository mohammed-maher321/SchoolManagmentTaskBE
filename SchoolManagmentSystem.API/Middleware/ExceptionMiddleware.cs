using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using SchoolManagmentSystem.Application.Common.Exceptions;
using System.Diagnostics;
using System.Net;
using Serilog;
using Newtonsoft.Json;

namespace SchoolManagmentSystem.API.Middleware;

public class ExceptionMiddleware
{
    private readonly Stopwatch _timer;
    private readonly RequestDelegate _next;
    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
        _timer = new Stopwatch();
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            Log.Logger.Information("ClinicMS Request : {Name} {@Request}", context.Request.GetDisplayUrl(), context.Request);

            _timer.Start();

            await _next(context);
            _timer.Stop();

            if (_timer.ElapsedMilliseconds > 500)
            {
                Log.Logger.Information("ClinicMS Request : {Name} {@Request} and time of request {time}", context.Request.GetDisplayUrl(), context.Request, _timer.ElapsedMilliseconds);
            }

        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
            LogError(ex, context);
        }
    }

    private void LogError(Exception ex, HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        string controllerName = null;
        string actionName = null;
        if (endpoint != null)
        {

            var controllerActionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            if (controllerActionDescriptor != null)
            {
                controllerName = controllerActionDescriptor.ControllerName;
                actionName = controllerActionDescriptor.ActionName;
            }
        }

        var exception = ex.Message;
        var stackTrace = ex.StackTrace ?? string.Empty;
        var targetSite = ex.TargetSite?.ToString() ?? string.Empty;
        var ip = context.Request != null ? context.Connection.RemoteIpAddress.MapToIPv4().ToString() : string.Empty;
        var email = context.User.Claims.FirstOrDefault(s => s.Type == "FullName")?.Value + "," + context.User.Claims.FirstOrDefault(s => s.Type == "Email")?.Value;
        var userAgent = context.Request != null ? context.Request.Headers["User-Agent"].ToString() : string.Empty;

        Log.Logger.Error("Exception", exception, stackTrace, targetSite, controllerName, actionName, email, ip, userAgent);
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;

        var result = string.Empty;

        switch (exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonConvert.SerializeObject(validationException.Failures);
                break;
            
            case NotFoundException _:
                code = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty)
        {
            result = JsonConvert.SerializeObject(new { error = exception.Message });
        }

        return context.Response.WriteAsync(result);
    }
}

public static class CustomExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}
