using System;
using GDE.Web.Models;
using GDE.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Web.Pages.Shared
{
    public class LoginController : Controller
    {
        private IUserService userService;

        public LoginController(IUserService userService)
        {
            this.userService = userService;
        }
        
        // GET
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AuthenticateUser(AuthenticationModel model)
        {
            Console.WriteLine("got post request from somewhere");
            var user = userService.Login(model.Username, model.Password);

            if (user == null)
            {
                ViewBag["Message"] = "Invalid username or password";
                return View("/");
            }
            
            ViewData["Account"] = user;
            return View("/");
        }
    }
}