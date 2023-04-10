using Mixi.Services;
using Mixi.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mixi.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;
using Microsoft.AspNetCore.Cors.Infrastructure;

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
        private readonly IProductServices productServices;
        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
            mixiDbContext = new MixiDbContext();
            userServices = new UserServices();
            roleServices = new RoleServices();
            cartServices = new CartServices();
            cartDetailServices = new CartDetailServices();
            productServices = new ProductServices();
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
                TempData["Login"] = "Chào mừng " + HttpContext.Session.GetString("name");
                var cart = SessionServices.GetObjFomSession(HttpContext.Session, "Cart");
                if (cart.Count == 0)
                {
                    List<CartDetail> cartDetails = cartDetailServices.GetAllCartDetail().Where(x => x.UserID == data.UserID).ToList();
                    HttpContext.Session.SetString("itemCount", cartDetails.Count().ToString());
                }
                else
                {
                    foreach (var item in cart)
                    {
                        var product = productServices.GetProductById(item.ProductID);
                        var existingProduct = cartDetailServices.GetAllCartDetail().FirstOrDefault(x => x.ProductID == item.ProductID && x.UserID == data.UserID);
                        if (existingProduct != null)
                        {
                            if (existingProduct != null)
                            {
                                //Kiểm tra số lượng vs số lượng tồn
                                if (existingProduct.Quantity + item.Quantity <= product.AvailableQuantity)
                                {
                                    // Nếu sản phẩm đã có trong giỏ hàng thì tăng số lượng lên 1
                                    existingProduct.Quantity += item.Quantity;
                                }
                                else
                                {
                                    existingProduct.Quantity = item.AvailableQuantity;
                                }
                                cartDetailServices.UpdateCartDetail(existingProduct);
                                SessionServices.RemoveSession(HttpContext.Session, "Cart");
                            }
                        }
                        else
                        {
                            CartDetail cartDetail = new CartDetail()
                            {
                                CartID = new Guid(),
                                UserID = data.UserID,
                                ProductID = item.ProductID,
                                Quantity = item.Quantity,
                            };
                            cartDetailServices.CreateCartDetail(cartDetail);
                            SessionServices.RemoveSession(HttpContext.Session, "Cart");
                        }
                    };
                    List<CartDetail> cartDetails = cartDetailServices.GetAllCartDetail().Where(x => x.UserID == data.UserID).ToList();
                    HttpContext.Session.SetString("itemCount", cartDetails.Count().ToString());
                }
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
                Cart cart = new Cart()
                {
                    UserID = _user.UserID,
                    Description = "",
                };
                cartServices.CreateCart(cart);
                TempData["Register"] = "Đăng ký thành công";
                return View();
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
