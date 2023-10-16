using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;

namespace Middleware.Middleware
{
    public static class ErrorMiddleware
    {
        public static IApplicationBuilder UseErrorHandler(this IApplicationBuilder appBuilder)
        {
            return appBuilder.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature != null)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.LoopDetected;
                        context.Response.ContentType = "application/json";

                        var json = new
                        {
                            message = exceptionHandlerFeature.Error.Message,
                        };

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(json));
                    }
                });
            });
        }
    }
}
