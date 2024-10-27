using System.Net;
using System.Text.Json;
using BioFarm.API.Errors;

namespace BioFarm.API.Middleware;

public class ExceptionMiddleware(IHostEnvironment env, RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context) 
    {
        try
        {
            await next(context);
        }
        catch (Exception ex) 
        {
            await HandleExceptionAsync(context, ex, env);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex, IHostEnvironment env)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = env.IsDevelopment()
        ? new ApiErrorResponse((HttpStatusCode)context.Response.StatusCode, ex.Message, ex.StackTrace) 
        : new ApiErrorResponse((HttpStatusCode)context.Response.StatusCode, ex.Message, "Internal Server Error");

        var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
        var json = JsonSerializer.Serialize(response, options);

        return  context.Response.WriteAsync(json);
    }
}
