using System;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MQTT.Api.Controllers.Mqtt;
using MQTT.Api.Options;
using MQTT.Api.Services;
using MQTT.Data;
using MQTT.Shared.Profiles;
using MQTTnet.AspNetCore.AttributeRouting;
using MQTTnet.AspNetCore.Extensions;

namespace MQTT.Api.Extensions
{
    public static class ConfigureExtensions
    {
        public static void AuthenticationConfiguration(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.Issuer,

                        ValidateAudience = true,
                        ValidAudience = AuthOptions.Audience,
                        ValidateLifetime = true,

                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });
        }

        public static void DatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MQTTDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                /*options.EnableDetailedErrors();
                options.LogTo(Console.WriteLine);
                options.EnableSensitiveDataLogging();*/
            }, ServiceLifetime.Transient);
        }

        public static void AutoMapperConfiguration(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new DeviceProfile());
                mc.AddProfile(new EventDeviceProfile());
                mc.AddProfile(new EventUserProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new MeasurementProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void SwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "API", Version = "v1"});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: '{token}')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }


        public static void MqttConfiguration(this IServiceCollection services)
        {
            services.AddCors();

            // Add Singleton MQTT Server object
            services.AddSingleton<MqttService>();

            // Add the MQTT Controllers
            services.AddMqttControllers();

            // Add the MQTT Service
            services
                .AddHostedMqttServerWithServices(aspNetMqttServerOptionsBuilder =>
                {
                    aspNetMqttServerOptionsBuilder.WithAttributeRouting(
                        /* 
                            By default, messages published to topics that don't
                            match any routes are rejected. Change this to true
                            to allow those messages to be routed without hitting
                            any controller actions.
                        */
                        false
                    );
                    var mqttService = aspNetMqttServerOptionsBuilder.ServiceProvider.GetRequiredService<MqttService>();
                    mqttService.ConfigureMqttServerOptions(aspNetMqttServerOptionsBuilder);
                    aspNetMqttServerOptionsBuilder.Build();
                })
                .AddMqttConnectionHandler()
                .AddConnections()
                .AddMqttWebSocketServerAdapter();
            // Add Scoped Services
            services.AddScoped<MqttBaseController, DeviceController>();
        }

        public static void LoggerServiceConfiguration(this IServiceCollection services)
        {
            services.AddTransient<LoggerService>();
        }

        public static string ToSHA1(this string input)
        {
            
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
            
        }
    }
}