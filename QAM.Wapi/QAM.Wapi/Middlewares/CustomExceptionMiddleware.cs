using QAM.Services;
using FluentValidation;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Diagnostics;
using System.Net;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace QAM.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _loggerService;
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService loggerService)
        {
            _next = next;
            _loggerService = loggerService;
        }
        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew();
            try
            {
                // Request degerleri console ekranına yazdırırlır.
                string message = "[Request] HTTP: " + context.Request.Method + " - " + context.Request.Path;
                _loggerService.Write(message);

                await _next.Invoke(context);
                watch.Stop();

                // Response degerleri console ekranına yazdırırlır.
                message = "[Response] HTTP: " + context.Request.Method + " - " + context.Request.Path +
                    " - " + context.Response.StatusCode + " in " + watch.Elapsed.TotalMilliseconds + " ms";
                _loggerService.Write(message);
            }
            catch (ValidationException validationEx)
            {

                // SeliLog ile validation hatalarınıın kayıt edilmesi
                Log.Error(validationEx, "ValidationError");
                Log.Error(
                    $"Path={context.Request.Path} || " +
                    $"Method={context.Request.Method} || " +
                    $"Exception={validationEx.Message}"
                );

                await HandleValidationException(context, validationEx);

            }
            catch (Exception ex)
            {

                // SeliLog ile hataların kayıt edilmesi
                Log.Error(ex, "UnexpectedError");
                Log.Fatal(
                    $"Path={context.Request.Path} || " +
                    $"Method={context.Request.Method} || " +
                $"Exception={ex.Message}"
                );

                await HandleException(context, ex);

            }

        }

        private Task HandleValidationException(HttpContext context, ValidationException validationEx)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            
            // Validasyon hataları geri dönülür
            var validationErrors = validationEx.Errors.Select(error =>
            {
                return new
                {
                    propertyName = error.PropertyName,
                    errorMessage = error.ErrorMessage
                };
            });

            var result = JsonConvert.SerializeObject(new { validationErrors }, Formatting.None);
            return context.Response.WriteAsync(result);
        }

        private Task HandleException(HttpContext context, Exception ex)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            //  hatalar geri dönülür
            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(result);
        }

    }
    static public class CustomExceptionMiddlewareExtention
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
   
}
