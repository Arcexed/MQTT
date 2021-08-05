using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EmptyMetanitWebApplication
{
    public class AuthenticationMiddleware
    {
        private RequestDelegate _next;
        public AuthenticationMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var token = context.Request.Query["token"];
            if (string.IsNullOrEmpty(token))
            {
                context.Response.StatusCode = 403;
            }
            else
            {
                await _next(context);
            }
        }
    }
}
