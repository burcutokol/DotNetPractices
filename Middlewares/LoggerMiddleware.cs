using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BookStoreWebApi.Middlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        public LoggerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context )
        {
            string message = "Executed action [Request] HTTP " + context.Request.Method + " - " + context.Request.Path; //writes executed endpoint type and its path
            Console.WriteLine(message);

            await _next(context); // executed called endpoint
         
        }

    }
    public static class LoggerMiddlewareExtension
    {
        public static IApplicationBuilder UseLoggerMiddlewareExtension(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggerMiddleware>();   
        }
    }
}
