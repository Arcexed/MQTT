#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GeekForLess_TestTask_Forum.Static;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models.Models;
using MQTT.Data;
using MQTT.Data.Entities;

// ReSharper disable NotAccessedField.Local

#endregion

namespace MQTT.Dashboard.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly MQTTDbContext _db;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public AccountController(MQTTDbContext db, IMapper mapper, ILogger logger)
        {
            _db = db;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Route("Login")]
        public ActionResult Login()
        {
            return User.Identity is { IsAuthenticated: true }
                ? RedirectToAction("Index", "Account")
                : View();
        }

        [HttpGet]
        [Route("Register")]
        public ActionResult Register()
        {
            return User.Identity is { IsAuthenticated: true } ? RedirectToAction("Index", "Account") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _db.Users.FirstOrDefaultAsync(u =>
                    u.Email == model.Email && u.Password == model.Password.ToSHA1());

                if (user?.Email != null)
                {
                    Authenticate(user);
                    return RedirectToAction("Index", "Dashboard");
                }

                ModelState.AddModelError("", "Incorrect email or password");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if (User.Identity is { IsAuthenticated: true })
                return RedirectToAction("Index", "Dashboard");

            if (ModelState.IsValid)
            {
                if (model.Password == model.ConfirmPassword)
                {
                    if (_db.Users.Any(u => u.Username.ToLower() == model.Username.ToLower()))
                    {
                        ViewBag.Message = "Username is busy. Try another";
                        ViewBag.isError = true;
                    }
                    else if (_db.Users.Any(u => u.Email.ToLower() == model.Email.ToLower()))
                    {
                        ViewBag.Message = "Email is busy. Try another";
                        ViewBag.isError = true;
                    }
                    else
                    {
                        var role = _db.Roles.FirstOrDefault(d => d.Name == "User");
                        var user = new User
                        {
                            Username = model.Username,
                            Email = model.Email,
                            Password = model.Password.ToSHA1(),
                            Ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty,
                            IsBlock = false,
                            AccessToken = Guid.NewGuid().ToString("N"),
                            Role = role!
                        };
                        _db.Users.Add(user);
                        _db.SaveChangesAsync();
                        ViewBag.Message = "Success";
                        ViewBag.isError = false;
                    }
                }
                else
                {
                    ViewBag.Message = "Password and confirm password does not match";
                    ViewBag.isError = true;
                }
            }
            else
            {
                ViewBag.Message = "One or more credentials is empty";
                ViewBag.isError = true;
            }

            return View();
        }

        [Authorize]
        [Route("Index")]
        public IActionResult Index()
        {
            var role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value;
            var name = ClaimsIdentity.DefaultNameClaimType;
            //string name = ClaimsIdentity.;
            return Content($"ваша роль: {role}\nName: {name}");
            //return View();
        }

        [Authorize]
        [Route("Profile")]
        public IActionResult Profile()
        {
            var username = User.Identity?.Name;
            var user = _db.Users.FirstOrDefault(d => d.Username == username);
            return View(user);
        }

        [Authorize]
        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        [HttpPost]
        [Route("Profile")]
        public async Task<IActionResult> Profile(User usr)
        {
            var user = _db.Users.First(s => s.Id == usr.Id);

            if (_db.Users.Any(d => d.AccessToken == usr.AccessToken))
                return View(user);

            if (_db.Users.Any(d => d.Username == usr.Username))
                return View(user);

            await _db.SaveChangesAsync();
            return RedirectToAction("Profile");
        }

        private void Authenticate(User user)
        {
            IList<Claim> claims = new List<Claim>
            {
                new(ClaimTypes.Sid, user.Id.ToString()),
                new(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new(ClaimTypes.Email, user.Email),
                new(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };
            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}