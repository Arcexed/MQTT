using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;

namespace EmptyMetanitWebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //using (var host = WebHost.Start("http://localhost:8080", context => context.Response.WriteAsync("Hi")))
            //{
            //    Console.WriteLine("Application has been started");
            //    host.WaitForShutdown();
            //}

            CreateHostBuilder(args).Build().Run();
            //var host = new WebHostBuilder()
            //    .UseKestrel()
            //    .UseContentRoot(Directory.GetCurrentDirectory())
            //    .UseIISIntegration()
            //    .UseStartup<Startup>()
            //    .Build();
            //host.Run();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
