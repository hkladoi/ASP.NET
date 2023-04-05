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
        private readonly ILogger<AccountController> _logger;
        private readonly MixiDbContext mixiDbContext;
        private readonly IUserServices userServices;
        private readonly IRoleServices roleServices;
        private readonly ICartServices cartServices;
        private readonly ICartDetailServices cartDetailServices;
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
            mixiDbContext = new MixiDbContext();
            userServices = new UserServices();
            roleServices = new RoleServices();
            cartServices = new CartServices();
            cartDetailServices = new CartDetailServices();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string account, string password, Guid id)
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
                TempData["Login"] = "Chào mừng " + HttpContext.Session.GetString("name");
                List<CartDetail> cartDetails = cartDetailServices.GetAllCartDetail().Where(x => x.UserID == data.UserID).ToList();
                string itemCount = cartDetails.Count().ToString();
                HttpContext.Session.SetString("itemCount", itemCount);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                TempData["Wrong"] = "Thông tin tài khoản không chính xác";
                return RedirectToAction("Login");
            }
        }
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("acc");
            HttpContext.Session.Remove("role");
            HttpContext.Session.Remove("name");
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

            var email = mixiDbContext.Users.FirstOrDefault(s => s.Email == _user.Email);
            var acc = mixiDbContext.Users.FirstOrDefault(c => c.Account == _user.Account);
            if (email == null && acc == null)
            {
                userServices.Createkach(_user);
                return RedirectToAction("Login");
            }
            else
            {
                TempData["email"] = "Email đã tồn tại";
                TempData["acc"] = "Account đã tồn tại";
                return View();
            }
        }

    }
}
