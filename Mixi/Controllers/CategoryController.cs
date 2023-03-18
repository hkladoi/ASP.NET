using Mixi.Services;
using Mixi.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mixi.Models;
using System.Diagnostics;

namespace Mixi.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly ICategoryServices categoryServices;
        public CategoryController(ILogger<CategoryController> logger)
        {
            _logger = logger;
            categoryServices = new CategoryServices();
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            //lấy product tywf db dựa theo id truyền vào route
            Category p = categoryServices.GetCategoryById(id);
            return View(p);
        }
        public IActionResult Edit(Category p)
        {
            if (categoryServices.CreateCategory(p))
            {
                return RedirectToAction("ShowlistCategory");
            }
            else return BadRequest();
        }
        public IActionResult Create()//hiển thị
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category p)//thực hiện tạo mới
        {
            if (categoryServices.CreateCategory(p))
            {
                return RedirectToAction("ShowlistCategory");
            }
            else return BadRequest();
        }
        public IActionResult Delete(Guid id)
        {
            if (categoryServices.DeleteCategory(id))
            {
                return RedirectToAction("ShowlistCategory");
            }
            else return BadRequest();
        }

        public IActionResult ShowlistCategory()
        {
            //using (MixiDbContext c = new MixiDbContext())
            //{
            //    var lists = c.Products.Include("Size").Include("Color").ToList();
            //    return View(lists);
            //}
            List<Category> Category = categoryServices.GetAllCategory();
            return View(Category);
        }

        public IActionResult Details(Guid id)
        {
            MixiDbContext dbContext = new MixiDbContext();
            var category = dbContext.Categories.Find(id);
            return View(category);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
