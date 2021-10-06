using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MQTT.Api.Auth;
using MQTT.Data;

namespace MQTT.Api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    { 
        private readonly ILogger<AccountController> _logger;
        private readonly MQTTDbContext _db;

        public AccountController(ILogger<AccountController> logger, MQTTDbContext db)
        {
             _logger = logger;
            _db = db;
        }

        [HttpGet("/token")]
        public async Task<IActionResult> Token(string accessToken)
        {
            var identity = await GetIdentity(accessToken);

            if (identity == null)
            {
                _logger.Log(LogLevel.Information,$"{DateTime.Now.ToString("O")} Invalid token ({accessToken}) IP:{HttpContext.Request.Host.Host}");
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
            _logger.Log(LogLevel.Information,$"{DateTime.Now.ToString("O")} Success authentication ({accessToken}) IP:{HttpContext.Request.Host.Host}");
            return Ok(response);
        }

        private async Task<ClaimsIdentity?> GetIdentity(string accessToken)
        {
            
            var user = await _db.Users.Include(d=>d.Role).FirstOrDefaultAsync(d=>d.AccessToken==accessToken);
            if (user is { IsBlock: false })
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
    }
}