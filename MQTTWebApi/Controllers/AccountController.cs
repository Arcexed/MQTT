using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using MQTTWebApi.Auth;

namespace MQTTWebApi.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly MqttdbContext _db;
        public AccountController(ILogger<AccountController> logger,MqttdbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet("/token")]
        public IActionResult Token(string accessToken)
        {
            var identity = GetIdentity(accessToken);
            if (identity == null)
            {   
                return BadRequest(new { errorText = "Invalid token." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name,
                nowBefore=DateTime.Now,
                expires = now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME))
            };

            return Json(response);
        }

        private ClaimsIdentity? GetIdentity(string accessToken)
        {
            User? user = _db.Users.Include(d=>d.Role).FirstOrDefault(x => x.AccessToken == accessToken);
            if (user != null)
            {
                if (!user.IsBlock)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
                    };
                    ClaimsIdentity claimsIdentity =
                        new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                            ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
            }

            // если пользователя не найдено
            return null;
        }
    }
}
