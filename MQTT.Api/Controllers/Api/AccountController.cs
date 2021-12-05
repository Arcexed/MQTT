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
using Microsoft.IdentityModel.Tokens;
using MQTT.Api.Contracts.v1.Request.AccountController;
using MQTT.Api.Contracts.v1.Response.AccountController;
using MQTT.Api.Extensions;
using MQTT.Api.Options;
using MQTT.Api.Services;
using MQTT.Data;
using Swashbuckle.AspNetCore.Annotations;
using static MQTT.Api.Options.ApiRoutes;
namespace MQTT.Api.Controllers.Api
{

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
        [ProducesResponseType(typeof(UserLoginResponse), 200)]
        [SwaggerOperation("Authentication user")]
        [Route(Account.Login)]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var identity = await GetIdentity(request.Email, request.Password, "Token");

            if (identity == null)
            {
                _loggerService.Log(
                    $"{DateTime.Now.ToString(CultureInfo.CurrentCulture)} Invalid username = {request.Email} and password = {request.Password} IP:{HttpContext.Request.Host.Host}");
                _loggerService.LogEvent(
                    $"Invalid username = {request.Email} and password = {request.Password} IP:{HttpContext.Request.Host.Host}");
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

            var response = new UserLoginResponse()
            {
                AccessToken = encodedJwt,
                UserId = identity.Name,
                NowBefore = DateTime.Now,
                Expires = now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
            };
            
            _loggerService.Log(
                $"{DateTime.Now.ToString("O")} Success login username = {request.Email} and password = {request.Password} IP:{HttpContext.Request.Host.Host}");
            _loggerService.LogEvent(
                $"Success login username = {request.Email} and password = {request.Password} IP:{HttpContext.Request.Host.Host}");

            return Ok(response);
        }

        private async Task<ClaimsIdentity?> GetIdentity(string email, string password, string authenticationType)
        {
            var user = await _db.Users.Include(d => d.Role)
                .FirstOrDefaultAsync(d => d.Email == email && d.Password == password.ToSHA1());
            if (user is {IsBlock: false})
            {
                var claims = new List<Claim>
                {
                    new(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
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