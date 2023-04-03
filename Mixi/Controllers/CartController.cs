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
        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
            mixiDbContext = new MixiDbContext();
            productServices = new ProductServices();
            colorServices = new ColorServices();
            categoryServices = new CategoryServices();
            imageServices = new ImageServices();
            sizeServices = new SizeServices();
        }
        //[HttpPost]
        public IActionResult AddToCart(Guid id, int quantity)
        {
            var product = productServices.GetProductById(id);
            var colorName = colorServices.GetAllColor().Where(c => c.ColorID == product.ColorID).Select(c => c.Name).FirstOrDefault();
            var categoryName = categoryServices.GetAllCategory().Where(c => c.CategoryID == product.CategoryID).Select(c => c.Name).FirstOrDefault();
            var sizeName = sizeServices.GetAllSize().Where(c => c.SizeID == product.SizeID).Select(c => c.Name).FirstOrDefault();
            var Image = imageServices.GetAllImage().Where(c => c.ImageID == product.ImageID).Select(c => c.LinkImage).FirstOrDefault();

            //lấy dữ liệu cart session
            var products = SessionServices.GetObjFomSession(HttpContext.Session, "Cart");
            var existingProduct = products.FirstOrDefault(x => x.ProductID == id);
            if (existingProduct != null)
            {
                //Kiểm tra số lượng vs số lượng tồn
                if (existingProduct.Quantity == product.AvailableQuantity)
                {
                    TempData["quantity"] = "Số lượng bạn chọn đã đạt mức tối đa của sản phẩm này";
                    existingProduct.Quantity = product.AvailableQuantity;
                }
                else
                {
                    // Nếu sản phẩm đã có trong giỏ hàng thì tăng số lượng lên 1
                    existingProduct.Quantity += 1;
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
                    Quantity = 1

                };
                products.Add(cart);
            }
            //gắn giá trị vào session
            SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
            return RedirectToAction("Details", "Home", new { id = product.ProductID });
            //return Ok();
            //return Json(new { success = true });

        }
        public IActionResult ShowCart()
        {

            var products = SessionServices.GetObjFomSession(HttpContext.Session, "Cart");
            decimal totalPrice = products.Sum(x => x.SalePrice > 0 ? (x.Price - x.SalePrice) * x.Quantity : x.Price * x.Quantity);
            ViewData["totalPrice"] = totalPrice;
            return View(products);
        }
    }
}
