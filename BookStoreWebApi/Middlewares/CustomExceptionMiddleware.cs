﻿using BookStoreWebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace BookStoreWebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerService _logger;
        public CustomExceptionMiddleware(RequestDelegate next, ILoggerService logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            var watch = Stopwatch.StartNew(); //time that request comes
            try
            {
               
                string message = "[Request] HTTP " + context.Request.Method + " - " + context.Request.Path; //writes executed endpoint type and its path
                _logger.Write(message);
                

                await _next(context); // executed called endpoint
                watch.Stop(); //ended time process of called endpoint

                message = "[Response] HTTP" + context.Request.Method + " - " + context.Request.Path +
                    " responded " + context.Response.StatusCode + " in " + watch.Elapsed.TotalSeconds;
                _logger.Write(message);
            }
            catch (Exception ex) 
            {
                watch.Stop();
                await HandleException(context, ex, watch);
            }
            
        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = "[Error]  HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message " + ex.Message + " in " + watch.Elapsed.TotalMilliseconds;
            _logger.Write(message);

            var res = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(res);

        }
    }
    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddle (this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}
