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

namespace Mixi.Controllers
{

    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly MixiDbContext mixiDbContext;
        private readonly IProductServices productServices;
        public CartController(ILogger<CartController> logger)
        {
            _logger = logger;
            mixiDbContext = new MixiDbContext();
            productServices = new ProductServices();
        }
        public IActionResult AddToCart(Guid id)
        {
            var product = productServices.GetProductById(id);
            var colorName = mixiDbContext.Colors
                .Where(c => c.ColorID == product.ColorID)
                .Select(c => c.Name)
                .FirstOrDefault();
            var categoryName = mixiDbContext.Categories
                .Where(c => c.CategoryID == product.CategoryID)
                .Select(c => c.Name)
                .FirstOrDefault();
            var sizeName = mixiDbContext.Sizes
                .Where(c => c.SizeID == product.SizeID)
                .Select(c => c.Name)
                .FirstOrDefault();
            var Image = mixiDbContext.Images
                .Where(c => c.ImageID == product.ImageID)
                .Select(c => c.Name)
                .FirstOrDefault();

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
                // Nếu chưa có thì thêm sản phẩm vào giỏ hàng
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
            return RedirectToAction("ShowCart");
            //int quantity = 1;
            //if (SessionServices.CheckObjInList(id, products))
            //{
            //    quantity = products.First(x => x.ProductID == id).Quantity + 1;
            //}
            //var cart = new CartViewModel()
            //{
            //    ProductID = product.ProductID,
            //    Name = product.Name,
            //    Price = product.Price,
            //    ProductCode = product.ProductCode,
            //    SalePrice = product.SalePrice,
            //    ColorName = colorName,
            //    CategoryName = categoryName,
            //    SizeName = sizeName,
            //    Image = Image,
            //    Quantity = 1
            //};
            ////kiểm tra dữ liệu
            //if (products.Count == 0)
            //{
            //    products.Add(cart);
            //    //gắn giá trị vào session
            //    SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
            //}
            //else
            //{
            //    if (SessionServices.CheckObjInList(id, products))
            //    {
            //        return Content("có rồi");
            //    }
            //    else
            //    {
            //        products.Add(cart);
            //        //chưa có sản phẩm thì thêm
            //        SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
            //    }
            //}
            //return RedirectToAction("ShowCart");
        }
        public IActionResult ShowCart()
        {

            var products = SessionServices.GetObjFomSession(HttpContext.Session, "Cart");
            return View(products);
        }
    }
}
