using AutoMapper;
using GeekForLess_TestTask_Forum.Static;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Models;
using MQTTDashboard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace MQTTDashboard.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger _logger;
        private readonly mqttdb_newContext _db;
        private readonly IMapper _mapper;
        public AccountController(mqttdb_newContext db,IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Account");
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Account");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = model.Password.ToSHA1();   
                var user = _db.Users.Include(d=>d.IdRoleNavigation).FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password.ToSHA1());
                var roleName = _db.Roles.FirstOrDefault(d=>d.Users.FirstOrDefault(d=>d.Email==model.Email).Email==model.Email);
                //var userViewModel = _mapper.Map<UserViewModel >(user);
                if (user.Email == model.Email)
                {
                    Authenticate(user);
                    return RedirectToAction("Index", "Account");
                }
                ModelState.AddModelError("", "Incorrect email or password");

            }
            return View();  
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Account");
            }
            if (ModelState.IsValid)
            {
                if (model.Password == model.ConfirmPassword)
                {
                    if (_db.Users.Select(u => u.Username.ToLower() == model.Username.ToLower()).Any())
                    {
                        ViewBag.Message = "Username is busy. Try another";
                        ViewBag.isError = true;
                        return View();
                    }

                    if (_db.Users.Select(u => u.Email.ToLower() == model.Email.ToLower()).Any())
                    {
                        ViewBag.Message = "Email is busy. Try another";
                        ViewBag.isError = true;
                        return View();
                    }

                    Role role = _db.Roles.Where(d => d.Name == "User").FirstOrDefault();
                    User user = new User()
                    {
                        Username = model.Username,
                        Email = model.Email,
                        Password = model.Password.ToSHA1(),
                        Ip = this.HttpContext.Connection.RemoteIpAddress.ToString(),
                        IsBlock = false,
                        AccessToken = Guid.NewGuid().ToString("N"),
                        IdRoleNavigation = role
                    };
                    _db.Users.Add(user); 
                    _db.SaveChangesAsync();
                    ViewBag.Message = "Success";
                    ViewBag.isError = false;

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
        public ActionResult Index()
        {
            string role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;
            string name = ClaimsIdentity.DefaultNameClaimType;
            //string name = ClaimsIdentity.;
            return Content($"ваша роль: {role}\nName: {name}");
            return View();
        }

        [Authorize]
        public IActionResult Profile()
        {
            string username = User.Identity.Name;
            var user = _db.Users.FirstOrDefault(d => d.Username == username);
            return View(user);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Profile(User usr)
        {
            //update student in DB using EntityFramework in real-life application
            
            //update list by removing old student and adding updated student for demo purpose
            var user = _db.Users.First(s => s.Id == usr.Id);

            if (_db.Users.Any(d => d.AccessToken == usr.AccessToken))
            {
                return View(user);
            }
            if (_db.Users.Any(d => d.Username == usr.Username))
            {
                return View(user);
            }
            //if (_db.Users.Any(d => d.Email == usr.Email))
            //{
            //    return View(user);
            //}


            _db.SaveChanges();
            return RedirectToAction("Profile");
        }
        private void Authenticate(User user)
        {
            IList<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()), 
                new Claim(ClaimsIdentity.DefaultNameClaimType,user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,user.IdRoleNavigation.Name),
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
}
