using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ThirdEmptyWebApplication.Services;

namespace ThirdEmptyWebApplication
{
    public class CounterMiddleware
    {
        private int i = 0;
        private RequestDelegate _next;
        public CounterMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICounter counter, CounterService counterService)
        {
            i++;
            await context.Response.WriteAsync(
                $"Request:{i}\nICounter:{counter.Value}\nCounterService:{counterService.Counter.Value}");
        }
    }
}
