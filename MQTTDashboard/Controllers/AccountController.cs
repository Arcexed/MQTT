using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MQTTDashboard.Models;

namespace MQTTDashboard.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost] [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Account");
            return View();
        }
        [HttpPost] [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

    }
}
