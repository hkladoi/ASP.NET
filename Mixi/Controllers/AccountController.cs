using Mixi.Services;
using Mixi.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mixi.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;

namespace Mixi.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly MixiDbContext mixiDbContext;
        private readonly IUserServices userServices;
        private readonly IRoleServices roleServices;

        public AccountController(ILogger<CategoryController> logger)
        {
            _logger = logger;
            mixiDbContext = new MixiDbContext();
            userServices = new UserServices();
            roleServices = new RoleServices();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string account, string password)
        {
            //if (ModelState.IsValid)
            //{
            var data = mixiDbContext.Users.Include("Roles").FirstOrDefault(s => s.Account == account && s.Password == password);
            if (data != null)
            {
                //add session
                HttpContext.Session.SetString("acc", data.Account);
                HttpContext.Session.SetString("role", data.Roles.RoleName);
                HttpContext.Session.SetString("name", data.FirstName + " " + data.LastName);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Login");
            }
            //}
            //return View();
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("acc");
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            ViewBag.Role = new SelectList(roleServices.GetAllRole(), "RoleID", "RoleName");
            return View();
        }
        [HttpPost]
        public ActionResult Register(User _user)
        {

            var check = mixiDbContext.Users.FirstOrDefault(s => s.Email == _user.Email);
            if (check == null)
            {
                //objModel.Configuration.ValidateOnSaveEnabled = false;
                userServices.CreateUser(_user);
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.error = "Email already exists";
                return View();
            }
        }

    }
}
