using Microsoft.AspNetCore.Mvc;
using Mixi.Models;
using System.Diagnostics;
using Mixi.Services;
using Mixi.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mixi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductServices productServices;
        private readonly MixiDbContext mixiDbContext;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            productServices = new ProductServices();
            mixiDbContext = new MixiDbContext();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
        public IActionResult Abount()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            ViewBag.Color = new SelectList(mixiDbContext.Colors.ToList(), "ColorID", "Name", "Status");
            ViewBag.Size = new SelectList(mixiDbContext.Sizes.ToList(), "SizeID", "Name", "Status");
            ViewBag.Category = new SelectList(mixiDbContext.Categories.ToList(), "CategoryID", "Name", "Status");
            ViewBag.Images = new SelectList(mixiDbContext.Images.ToList(), "ImageID", "LinkImage");
            Product p = productServices.GetProductById(id);
            return View(p);
        }
        public IActionResult Edit(Product p)
        {
            if (productServices.CreateProduct(p))
            {
                return RedirectToAction("ShowlistProduct");
            }
            else return BadRequest();
        }
        public ActionResult Create()//hiển thị
        {
            ViewBag.Color = new SelectList(mixiDbContext.Colors.ToList(), "ColorID", "Name", "Status");
            ViewBag.Size = new SelectList(mixiDbContext.Sizes.ToList(), "SizeID", "Name", "Status");
            ViewBag.Category = new SelectList(mixiDbContext.Categories.ToList(), "CategoryID", "Name", "Status");
            ViewBag.Images = new SelectList(mixiDbContext.Images, "ImageID", "LinkImage");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product p)//thực hiện tạo mới
        {
            if (productServices.CreateProduct(p))
            {
                return RedirectToAction("ShowlistProduct");
            }
            else return BadRequest();
        }
        public IActionResult Delete(Guid id)
        {
            if (productServices.DeleteProduct(id))
            {
                return RedirectToAction("ShowlistProduct");
            }
            else return BadRequest();
        }
        public IActionResult Show()
        {
            Product product = new Product() { ProductID = Guid.NewGuid(), Name = "hkladoi", ProductCode = "HK1", Price = 1000, SalePrice = 100, AvailableQuantity = 1, Supplier = "HN", Description = "co san", Status = 0 };
            return View(product);
        }

        public IActionResult ShowlistProduct()
        {
            using (MixiDbContext c = new MixiDbContext())
            {
                var lists = c.Products.Include("Size").Include("Color").Include("Category").Include("Images").ToList();
                return View(lists);
            }
            //List<Product> products = productServices.GetAllProduct();
            //return View(products);
        }

        public IActionResult Details(Guid id)
        {
            MixiDbContext dbContext = new MixiDbContext();
            var product = dbContext.Products.Find(id);
            return View(product);
        }
        public IActionResult Redirect()
        {
            return RedirectToAction("Test");//thực hiện điều hướng đến 1 action nào đó
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}