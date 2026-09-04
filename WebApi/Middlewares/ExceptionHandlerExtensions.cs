namespace WebApi.Middlewares;

using Microsoft.AspNetCore.Diagnostics;

public static class ExceptionHandlerExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app, IHostEnvironment environment)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                if (contextFeature != null)
                {
                    var statusCode = contextFeature.Error switch
                    {
                        ArgumentException => StatusCodes.Status400BadRequest,
                        KeyNotFoundException => StatusCodes.Status404NotFound,
                        _ => StatusCodes.Status500InternalServerError
                    };

                    context.Response.StatusCode = statusCode;
                    context.Response.ContentType = "application/json";

                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = statusCode,
                        Message = contextFeature.Error.Message,
                        Trace = contextFeature.Error.StackTrace
                    }.ToString());
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";
                }
            });
        });
    }
}