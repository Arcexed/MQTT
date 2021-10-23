using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MQTT.Api.Auth;
using MQTT.Api.Services;
using MQTT.Data;
using MQTT.Data.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace MQTT.Api.Controllers.Api
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MQTTDbContext _db;
        private readonly LoggerService _loggerService;
        public AccountController(MQTTDbContext db, LoggerService loggerService)
        {
            _db = db;
            _loggerService = loggerService;
        }

        [HttpPost]
        [AllowAnonymous]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(JsonResult), 200)]
        [SwaggerOperation("Authentication user")]
        [Route("token")]
        public async Task<IActionResult> Token([Required] string accessToken)
        {
            var identity = await GetIdentity(accessToken);

            if (identity == null)
            {
                _loggerService.Log($"{DateTime.Now.ToString(CultureInfo.CurrentCulture)} Invalid token ({accessToken}) IP:{HttpContext.Request.Host.Host}");
                _loggerService.LogEvent($"Invalid token ({accessToken}) IP:{HttpContext.Request.Host.Host}");
                return BadRequest("Invalid token.");
            }

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                AuthOptions.Issuer,
                AuthOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                nowBefore = DateTime.Now,
                expires = now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime))
            };
            _loggerService.Log($"{DateTime.Now.ToString("O")} Success authentication ({accessToken}) IP:{HttpContext.Request.Host.Host}");
            _loggerService.LogEvent($"Invalid token ({accessToken}) IP:{HttpContext.Request.Host.Host}");

            return Ok(response);
        }

        private async Task<ClaimsIdentity?> GetIdentity(string accessToken)
        {
            var user = await _db.Users.Include(d => d.Role).FirstOrDefaultAsync(d => d.AccessToken == accessToken);
            if (user is {IsBlock: false})
            {
                var claims = new List<Claim>
                {
                    new(ClaimsIdentity.DefaultNameClaimType, user.Username),
                    new(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
                };
                ClaimsIdentity claimsIdentity =
                    new(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }


        [HttpGet]
        [AllowAnonymous]
        [SwaggerResponse((int) HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), 200)]
        [SwaggerOperation("database add some changes")]
        [Route("database")]
        public async Task<IActionResult> Database()
        {
            _db.Roles.Add(new Role()
            {
                Id = Guid.NewGuid(),
                Name = "User"
            });
            await _db.SaveChangesAsync();
            _db.Users.Add(new User()
            {
                Id = Guid.NewGuid(),
                Email = "arcex.off@gmail.com",
                Ip = "",
                Password = "test",
                Role = await _db.Roles.FirstAsync(d=>d.Name=="User"),
                AccessToken = "5356cf8159584b31864e1c8ba72232cb",
                IsBlock = false,
                Username = "Arcex",
            });
            await _db.SaveChangesAsync();
            _db.Devices.Add(new Device()
            {
                Id = Guid.NewGuid(),
                Description = "Device for testing system",
                Geo = "Lomonosova, 67",
                Name = "TestDevice",
                CreatingDate = DateTime.Now,
                IsPublic = false,
                MqttToken = "3db1f91c8ba1475fb24c3c0ce62e1415",
                User = await _db.Users.FirstAsync(d=>d.Username=="Arcex")
            });
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}