using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP_Core_Template.Middleware
{
    //中間件
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Do something with context near the beginning of request processing.
            Console.WriteLine("LoggingMiddleware executing..");
            Console.WriteLine("Request: " + context.Request.Path);
            await _next(context);
            
            // Clean up.
            Console.WriteLine("LoggingMiddleware executed..");
            Console.WriteLine("Response: " + context.Response.StatusCode);
        }
    }
}