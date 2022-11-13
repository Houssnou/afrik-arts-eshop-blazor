using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly IHostEnvironment _env;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(Activity.Current?.Id ?? context.TraceIdentifier, ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                BaseResponse<string> response;
                response = _env.IsDevelopment()
                    ? new BaseResponse<string>
                    {
                        Success = false,
                        Data = null,
                        Errors = new List<string>()
                        {
                            Activity.Current?.Id ?? context.TraceIdentifier, ex.Message, ex.StackTrace.ToString()
                        }
                    }
                    : new BaseResponse<string>
                    {
                        Success = false,
                        Data = null,
                        Errors = new List<string>()
                        {
                            $"Oops! Something went wrong. please report this unexpected response using this code: { Activity.Current?.Id ?? context.TraceIdentifier}"
                        }
                    };
                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}