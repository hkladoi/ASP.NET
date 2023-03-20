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
        private readonly IColorServices colorServices;
        private readonly ISizeServices sizeServices;
        private readonly ICategoryServices categoryServices;
        private readonly IImageServices imageServices;
        private readonly MixiDbContext mixiDbContext;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            productServices = new ProductServices();
            colorServices = new ColorServices();
            sizeServices = new SizeServices();
            categoryServices = new CategoryServices();
            imageServices = new ImageServices();
            mixiDbContext = new MixiDbContext();
        }

        public IActionResult Index()
        {

            var lists = mixiDbContext.Products.Include("Size").Include("Color").Include("Category").Include("Images").ToList();
            return View(lists);

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
            ViewBag.Color = new SelectList(colorServices.GetAllColor(), "ColorID", "Name", "Status");
            ViewBag.Size = new SelectList(sizeServices.GetAllSize(), "SizeID", "Name", "Status");
            ViewBag.Category = new SelectList(categoryServices.GetAllCategory(), "CategoryID", "Name", "Status");
            ViewBag.Images = new SelectList(imageServices.GetAllImage(), "ImageID", "Name");
            Product p = productServices.GetProductById(id);
            return View(p);
        }
        public IActionResult Edit(Product p)
        {
            if (productServices.UpdateProduct(p))
            {
                return RedirectToAction("ShowlistProduct");
            }
            else return BadRequest();
        }
        public ActionResult Create()//hiển thị
        {
            ViewBag.Color = new SelectList(colorServices.GetAllColor(), "ColorID", "Name");
            ViewBag.Size = new SelectList(sizeServices.GetAllSize(), "SizeID", "Name");
            ViewBag.Category = new SelectList(categoryServices.GetAllCategory(), "CategoryID", "Name");
            ViewBag.Images = new SelectList(imageServices.GetAllImage(), "ImageID", "Name");
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

            var lists = mixiDbContext.Products.Include("Size").Include("Color").Include("Category").Include("Images").ToList();
            return View(lists);

            //List<Product> products = productServices.GetAllProduct();
            //return View(products);
        }

        public IActionResult Details(Guid id)
        {
            var product = mixiDbContext.Products.Where(c => c.ProductID == id).Include(c => c.Size).Include(c => c.Color).Include(c => c.Category).Include(c => c.Images).FirstOrDefault();
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