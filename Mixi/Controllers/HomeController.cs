using Microsoft.AspNetCore.Mvc;
using Mixi.Models;
using System.Diagnostics;
using Mixi.Services;
using Mixi.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Mixi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Delete(Guid id)
        {
            MixiDbContext dbContext = new MixiDbContext();
            var product = dbContext.Products.Find(id);
            return View();
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
                var lists = c.Products.Include("Size").Include("Color").ToList();
                return View(lists);
            }
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