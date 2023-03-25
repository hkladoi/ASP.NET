using Microsoft.AspNetCore.Mvc;
using Mixi.Models;
using System.Diagnostics;
using Mixi.Services;
using Mixi.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mixi.ViewModel;

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

            List<Category> Category = categoryServices.GetAllCategory();
            var lists = mixiDbContext.Products.Take(8).Include("Size").Include("Color").Include("Category").Include("Images").ToList();
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
            ViewBag.Color = new SelectList(colorServices.GetAllColor(), "ColorID", "Name", "Status");
            ViewBag.Size = new SelectList(sizeServices.GetAllSize(), "SizeID", "Name", "Status");
            ViewBag.Category = new SelectList(categoryServices.GetAllCategory(), "CategoryID", "Name", "Status");
            ViewBag.Images = new SelectList(imageServices.GetAllImage(), "ImageID", "Name");
            if (productServices.UpdateProduct(p))
            {
                return RedirectToAction("ShowlistProduct");
            }
            else
            {
                //TempData["AlertMessage"] = "lỗi đm";
                //return View();
                return BadRequest();
            }
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
        public ActionResult Show()
        {
            //var product = mixiDbContext.Products.Where(c => c.CategoryID == id).Include(c => c.Size).Include(c => c.Color).Include(c => c.Category).Include(c => c.Images).ToList();
            //return View(product);
            var viewModel = new ProductCategoryViewModel
            {
                Products = mixiDbContext.Products.Include("Size").Include("Color").Include("Category").Include("Images").ToList(),
                Categories = mixiDbContext.Categories.ToList()
            };
            return View(viewModel);
        }
        public IActionResult ShowProductByCategory(Guid id)
        {
            //var product = mixiDbContext.Products.Where(c => c.CategoryID == id).Include(c => c.Size).Include(c => c.Color).Include(c => c.Category).Include(c => c.Images).ToList();
            //return View(product);
            var viewModel = new ProductCategoryViewModel
            {
                Products = mixiDbContext.Products.Where(c => c.CategoryID == id).Include(c => c.Size).Include(c => c.Color).Include(c => c.Category).Include(c => c.Images).ToList(),
                Categories = mixiDbContext.Categories.ToList(),
                CategoryName = categoryServices.GetCategoryById(id).Name
            };
            return View(viewModel);
            //var lists = mixiDbContext.Products.Take(8).Include("Size").Include("Color").Include("Category").Include("Images").ToList();
            //return View(lists);
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
        public IActionResult ShowlistCategory()
        {

            List<Category> Category = categoryServices.GetAllCategory();
            return View(Category);
        }

        public IActionResult TransferData()//day du lieu qua cac view
        {
            //de truyen du lieu qua view thi ngoai cach truyen truc tiep 1 object model
            /*
             * su dung viewbag: du lieuj trong viewbag la du lieu dynamic
             * khong can khoi tao thanh phan ma dat ten luon
             * 
             */
            int[] marrk = { 1, 2, 3, 4, 5 };
            List<string> charec = new List<string>()
            {
                "naruto","xeko","sasuke"
            };
            /*
             * ViewData 
             * data se duoc truyen tai duoi dang key-value
             * du lieu lai o dang generic
             */
            /*
             * su dung session(phien lam viec), co che value-key
             */
            string massages = "shop db";
            HttpContext.Session.SetString("Messages", massages);
            string content = HttpContext.Session.GetString("Messages");
            ViewData["name"] = charec;//gan du lieu
            ViewBag.marrk = marrk;//gan du lieu
            ViewData["content"] = content;
            return View();
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