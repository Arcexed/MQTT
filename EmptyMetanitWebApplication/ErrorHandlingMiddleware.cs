using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EmptyMetanitWebApplication
{
    public class ErrorHandlingMiddleware
    {
        private RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
            if (context.Response.StatusCode == 403)
            {
                await context.Response.WriteAsync("Access Denied");
            }
            else if (context.Response.StatusCode == 404)
            {
                await context.Response.WriteAsync("Not Found");
            }
        }
    }
}
