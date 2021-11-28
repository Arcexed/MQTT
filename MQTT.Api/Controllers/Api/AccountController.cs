using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        public async Task<IActionResult> Token([Required] string email, [Required] string password)
        {
            var identity = await GetIdentity(email, password, "Token");

            if (identity == null)
            {
                _loggerService.Log($"{DateTime.Now.ToString(CultureInfo.CurrentCulture)} Invalid username = {email} and password = {password} IP:{HttpContext.Request.Host.Host}");
                _loggerService.LogEvent($"Invalid username = {email} and password = {password} IP:{HttpContext.Request.Host.Host}");
                return Unauthorized("Invalid credentials.");
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
            _loggerService.Log($"{DateTime.Now.ToString("O")} Success login username = {email} and password = {password} IP:{HttpContext.Request.Host.Host}");
            _loggerService.LogEvent($"Success login username = {email} and password = {password} IP:{HttpContext.Request.Host.Host}");

            return Ok(response);
        }

        private async Task<ClaimsIdentity?> GetIdentity(string email, string password, string authenticationType)
        {
            var user = await _db.Users.Include(d => d.Role).FirstOrDefaultAsync(d => d.Email == email && d.Password == password);
            if (user is {IsBlock: false})
            {
                var claims = new List<Claim>
                {
                    new(ClaimsIdentity.DefaultNameClaimType, user.Username),
                    new(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
                };
                ClaimsIdentity claimsIdentity =
                    new(claims, authenticationType, ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }


        
    }
}