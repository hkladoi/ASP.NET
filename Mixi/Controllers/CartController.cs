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
            //lấy dữ liệu
            var product = productServices.GetProductById(id);
            var colorName = mixiDbContext.Colors.Where(c => c.ColorID == product.ColorID).Select(c => c.Name).FirstOrDefault();
            var productViewModel = new ProductCategoryViewModel
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Price = product.Price,
                ProductCode = product.ProductCode,
                SalePrice = product.SalePrice,
                AvailableQuantity = product.AvailableQuantity,
                Status = product.Status,
                Supplier = product.Supplier,
                Description = product.Description,
                CategoryID = product.CategoryID,
                SizeID = product.SizeID,
                ImageID = product.ImageID,
                ColorID = product.ColorID,
                ColorName = colorName
            };
            //lấy dữ liệu cart session
            var products = SessionServices.GetObjFomSession(HttpContext.Session, "Cart");
            //kiểm tra dữ liệu
            if (products.Count == 0)
            {
                products.Add(product);
                //gắn giá trị vào session
                SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
            }
            else
            {
                if (SessionServices.CheckObjInList(id, products))
                {
                    return Content("có rồi");
                }
                else
                {
                    products.Add(product);
                    //chưa có sản phẩm thì thêm
                    SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
                }
            }
            return RedirectToAction("ShowCart");
        }
        public IActionResult ShowCart()
        {
            var products = SessionServices.GetObjFomSession(HttpContext.Session, "Cart");
            return View(products);
        }
    }
}
