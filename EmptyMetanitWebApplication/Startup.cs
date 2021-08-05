using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;

namespace EmptyMetanitWebApplication
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        //IWebHostEnvironment _env;
        //public Startup(IWebHostEnvironment env)
        //{
        //    _env = env;
        //}
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,IWebHostEnvironment env)
        {
            //if (env.IsEnvironment("Test"))
            //{
            //    app.Run(async (context) =>
            //    {
            //        context.Response.Headers["fsfs"] = "1234";
            //        await context.Response.WriteAsync("Project in test stage");
            //    });
            //}
            
            //app.UseStaticFiles();
            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(@"C:\"),

            //    RequestPath = new PathString("/pages")
            //});
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
                app.UseHsts();

            app.UseHttpsRedirection();
            app.UseMiddleware<TestMiddleware>();
            //app.UseMiddleware<ErrorHandlingMiddleware>();
            //app.UseMiddleware<AuthenticationMiddleware>();
            app.UseMiddleware<RoutingMiddleware>();
            app.UseDefaultFiles();


            // app.Run(HandleReturnIdTask);
        }



        private async Task HandleReturnIdTask(HttpContext context)
        {
            var host = context.Request.Host.Value;
            var path = context.Request.Path;
            var query = context.Request.QueryString.Value;
            var test = context.Request.Cookies.ToString();
            var protocol = context.Request.Protocol;
            var requestType = context.Request.Method;
            bool isGET = context.Request.isGET();
            string id = "";
            string resp = "";
            if (context.Request.Query.ContainsKey("id"))
            {
                int temp;
                bool isParsed = int.TryParse(context.Request.Query["id"], out temp);
                if (isParsed)
                {
                    id = $"{temp}";
                }
                else
                {
                    resp = "Error";
                }
            }
            else
            {
                resp = "Error";
            }


            resp += "\n" +
                    $"Host: {host}\n" +
                    $"Path: {path}\n" +
                    $"Query: {query}\n" +
                    $"Cookies: {test}\n" +
                    $"Protocol: {protocol}\n" +
                    $"Request Type: {requestType}\n" +
                    $"isGet: {isGET}\n";
            
            if (id != "")
            {
                await context.Response.WriteAsync($"id is {id}"+resp);
            }
            else
            {
                await HandleDefaultTask(context,resp);
            }
        }

        private async Task HandleDefaultTask(HttpContext context,string content)
        {
                var resp = context.Response;
                resp.Headers["testValue"] = "TestRespInHeader";
                await resp.WriteAsync("Good bye, World..\n"+content);
                await resp.CompleteAsync();
        }
    }


    public static class test
    {
        public static bool isGET(this HttpRequest request)
        {
            if (request.Method.ToLower() == "get")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
