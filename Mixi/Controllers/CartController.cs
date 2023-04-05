using Mixi.Services;
using Mixi.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mixi.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;
using NuGet.Protocol;
using Mixi.ViewModel;
using Microsoft.CodeAnalysis;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Plugins;

namespace Mixi.Controllers
{

    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly MixiDbContext mixiDbContext;
        private readonly IProductServices productServices;
        private readonly IColorServices colorServices;
        private readonly ICategoryServices categoryServices;
        private readonly IImageServices imageServices;
        private readonly ISizeServices sizeServices;
        private readonly ICartServices cartServices;
        private readonly ICartDetailServices cartDetailServices;
        private readonly IUserServices userServices;
        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
            mixiDbContext = new MixiDbContext();
            productServices = new ProductServices();
            colorServices = new ColorServices();
            categoryServices = new CategoryServices();
            imageServices = new ImageServices();
            sizeServices = new SizeServices();
            cartServices = new CartServices();
            cartDetailServices = new CartDetailServices();
            userServices = new UserServices();
        }
        [HttpPost]
        public IActionResult AddToCart(CartViewModel model)
        {
            var acc = HttpContext.Session.GetString("acc");
            var product = productServices.GetProductById(model.ProductID);
            var colorName = colorServices.GetAllColor().Where(c => c.ColorID == product.ColorID).Select(c => c.Name).FirstOrDefault();
            var categoryName = categoryServices.GetAllCategory().Where(c => c.CategoryID == product.CategoryID).Select(c => c.Name).FirstOrDefault();
            var sizeName = sizeServices.GetAllSize().Where(c => c.SizeID == product.SizeID).Select(c => c.Name).FirstOrDefault();
            var Image = imageServices.GetAllImage().Where(c => c.ImageID == product.ImageID).Select(c => c.LinkImage).FirstOrDefault();

            //lấy dữ liệu cart session
            var products = SessionServices.GetObjFomSession(HttpContext.Session, "Cart");
            var existingProduct = products.FirstOrDefault(x => x.ProductID == model.ProductID);
            if (acc == null)
            {
                if (existingProduct != null)
                {
                    //Kiểm tra số lượng vs số lượng tồn
                    if (existingProduct.Quantity + model.Quantity <= product.AvailableQuantity)
                    {
                        // Nếu sản phẩm đã có trong giỏ hàng thì tăng số lượng lên 1
                        existingProduct.Quantity += model.Quantity;
                    }
                    else
                    {
                        TempData["quantity"] = "Số lượng bạn chọn đã đạt mức tối đa của sản phẩm này";
                        existingProduct.Quantity = product.AvailableQuantity;
                    }
                }
                else
                {
                    var cart = new CartViewModel()
                    {
                        ProductID = product.ProductID,
                        Name = product.Name,
                        Price = product.Price,
                        ProductCode = product.ProductCode,
                        SalePrice = product.SalePrice,
                        ColorName = colorName,
                        CategoryName = categoryName,
                        SizeName = sizeName,
                        Image = Image,
                        Quantity = model.Quantity

                    };
                    products.Add(cart);
                }
                //gắn giá trị vào session
                SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
                return RedirectToAction("Details", "Home", new { id = product.ProductID });
            }
            return RedirectToAction("Details", "Home", new { id = product.ProductID });
        }
        [HttpPost]
        public IActionResult AddToCartUser(CartViewModel model)
        {
            var acc = HttpContext.Session.GetString("acc");
            var IdCart = userServices.GetAllUser().FirstOrDefault(c => c.Account == acc).UserID;
            var product = productServices.GetProductById(model.ProductID);
            var existing = cartDetailServices.GetAllCartDetail().FirstOrDefault(x => x.ProductID == product.ProductID && x.UserID == IdCart);
            if (existing != null)
            {
                //Kiểm tra số lượng vs số lượng tồn
                if (existing.Quantity + model.Quantity <= product.AvailableQuantity)
                {
                    // Nếu sản phẩm đã có trong giỏ hàng thì tăng số lượng lên 1
                    existing.Quantity += model.Quantity;
                }
                else
                {
                    TempData["quantityCartUser"] = "Số lượng bạn chọn đã đạt mức tối đa của sản phẩm này";
                    existing.Quantity = product.AvailableQuantity;
                }
                cartDetailServices.UpdateCartDetail(existing);
            }
            else
            {
                var cartDetails = new CartDetail();
                cartDetails.CartID = Guid.NewGuid();
                cartDetails.UserID = IdCart;
                cartDetails.ProductID = model.ProductID;
                cartDetails.Quantity = model.Quantity;
                cartDetailServices.CreateCartDetail(cartDetails);
            }
            List<CartDetail> cartDetail = cartDetailServices.GetAllCartDetail().Where(x => x.UserID == IdCart).ToList();
            string itemCount = cartDetail.Count().ToString();
            HttpContext.Session.Remove("itemCount");
            HttpContext.Session.SetString("itemCount", itemCount);
            return RedirectToAction("Details", "Home", new { id = product.ProductID });
        }

        [HttpPost]
        public IActionResult UpdateCart(Guid idsp, int sl)
        {
            var product = productServices.GetProductById(idsp);
            var products = SessionServices.GetObjFomSession(HttpContext.Session, "Cart");
            var existingProduct = products.FirstOrDefault(x => x.ProductID == idsp);
            var a = products.Find(c => c.ProductID == idsp);
            if (sl == product.AvailableQuantity)
            {
                TempData["quantityCart"] = "Số lượng bạn chọn đã đạt mức tối đa của sản phẩm này";
                existingProduct.Quantity = product.AvailableQuantity;
            }
            else a.Quantity = sl;

            SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
            return RedirectToAction("ShowCart");
        }
        [HttpPost]
        public IActionResult UpdateCartUser(Guid idsp, int sl)
        {
            var acc = HttpContext.Session.GetString("acc");
            var IdCart = userServices.GetAllUser().FirstOrDefault(c => c.Account == acc).UserID;
            var product = productServices.GetProductById(idsp);
            var existing = cartDetailServices.GetAllCartDetail().FirstOrDefault(x => x.ProductID == product.ProductID && x.UserID == IdCart);
            if (sl == product.AvailableQuantity)
            {
                TempData["quantityCart"] = "Số lượng bạn chọn đã đạt mức tối đa của sản phẩm này";
                existing.Quantity = product.AvailableQuantity;
            }
            else existing.Quantity = sl;
            cartDetailServices.UpdateCartDetail(existing);
            return RedirectToAction("ShowCartUser");
        }
        public IActionResult ShowCart()
        {
            var products = SessionServices.GetObjFomSession(HttpContext.Session, "Cart");
            decimal totalPrice = products.Sum(x => x.SalePrice > 0 ? (x.Price - x.SalePrice) * x.Quantity : x.Price * x.Quantity);
            ViewData["totalPrice"] = totalPrice;
            return View(products);
        }
        public IActionResult ShowCartUser()
        {
            var acc = HttpContext.Session.GetString("acc");
            var IdCart = userServices.GetAllUser().FirstOrDefault(c => c.Account == acc).UserID;
            List<CartDetail> cartDetails = cartDetailServices.GetAllCartDetail().Where(x => x.UserID == IdCart).ToList();
            decimal totalPrice = cartDetails.Sum(x => x.Product.SalePrice > 0 ? (x.Product.Price - x.Product.SalePrice) * x.Quantity : x.Product.SalePrice * x.Quantity);
            ViewData["totalPrice"] = totalPrice;
            return View(cartDetails);
        }
        public IActionResult DeleteCart(Guid id)
        {
            var acc = HttpContext.Session.GetString("acc");
            var IdCart = userServices.GetAllUser().FirstOrDefault(c => c.Account == acc).UserID;
            if (HttpContext.Session.GetString("acc") == null)
            {
                var products = SessionServices.GetObjFomSession(HttpContext.Session, "Cart");
                var x = products.FirstOrDefault(c => c.ProductID == id);
                products.Remove(x);
                SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);

            }
            else
            {
                cartDetailServices.DeleteCartDetail(id);
            }
            List<CartDetail> cartDetail = cartDetailServices.GetAllCartDetail().Where(x => x.UserID == IdCart).ToList();
            string itemCount = cartDetail.Count().ToString();
            HttpContext.Session.Remove("itemCount");
            HttpContext.Session.SetString("itemCount", itemCount);
            return RedirectToAction("ShowCartUser");
        }
        public IActionResult DeleteCartAll()
        {
            var acc = HttpContext.Session.GetString("acc");
            var IdCart = userServices.GetAllUser().FirstOrDefault(c => c.Account == acc).UserID;
            if (HttpContext.Session.GetString("acc") == null)
            {
                var products = SessionServices.GetObjFomSession(HttpContext.Session, "Cart");
                products.Clear();
                SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);

            }
            else
            {
                List<CartDetail> cartDetails = cartDetailServices.GetAllCartDetail().Where(x => x.UserID == IdCart).ToList();
                foreach (var item in cartDetails)
                {
                    cartDetailServices.DeleteCartDetail(item.CartID);
                }
            }
            List<CartDetail> cartDetail = cartDetailServices.GetAllCartDetail().Where(x => x.UserID == IdCart).ToList();
            string itemCount = cartDetail.Count().ToString();
            HttpContext.Session.Remove("itemCount");
            HttpContext.Session.SetString("itemCount", itemCount);
            return RedirectToAction("ShowCart");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
