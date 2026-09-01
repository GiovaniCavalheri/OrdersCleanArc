namespace WebApi.Middlewares;

using Microsoft.AspNetCore.Diagnostics;

public static class ExceptionHandlerExtensions
{
    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {

                context.Response.StatusCode = StatusCodes.Status500InternalServerError; 
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>();
                if (contextFeature != null)
                {
                    await context.Response.WriteAsync(new ErrorDetails()
                    {
                        StatusCode = contextFeature.Error switch
                        {
                            ArgumentException => StatusCodes.Status400BadRequest,
                            KeyNotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        },
                        Message = contextFeature.Error.Message,
                        Trace = contextFeature.Error.StackTrace
                    }.ToString());
                }
            });
        });
    }
}
