using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MQTTDashboard.Models.DbModels;
using MQTTDashboard.Models.Profiles;
using MQTTWebApi.Models;
using MQTTWebApi.Models.Profiles;

namespace MQTTDashboard
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            string reserveConnection = Configuration.GetConnectionString("ReserveConnection");
            services.AddDbContext<mqttdb_newContext>(options =>
            {
                options.UseSqlServer(connection);
                //options.UseSqlServer(reserveConnection);
                options.LogTo(d =>
                {
                    File.AppendAllText("logs_dashboard.log",d);
                });
                options.EnableSensitiveDataLogging();
            });
            // automapper
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DeviceProfile());
                mc.AddProfile(new EventDeviceProfile());
                mc.AddProfile(new MeasurementProfile());
                mc.AddProfile(new EventUserProfile());
                mc.AddProfile(new UserProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = new PathString("/Account/Login/");
              //  options.AccessDeniedPath = new PathString("/Account/Forbidden/");
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
