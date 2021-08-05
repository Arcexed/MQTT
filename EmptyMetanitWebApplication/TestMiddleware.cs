using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EmptyMetanitWebApplication
{
    public class TestMiddleware
    {
        private RequestDelegate _next;
        public TestMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            int x = 0;
            int y = 8 / x;
            await context.Response.WriteAsync($"Result = {y}");


        }
    }
}
