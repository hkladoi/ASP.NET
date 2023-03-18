using Mixi.Services;
using Mixi.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mixi.Models;
using System.Diagnostics;

namespace Mixi.Controllers
{
    public class SizeController : Controller
    {
        private readonly ILogger<SizeController> _logger;
        private readonly ISizeServices sizeServices;
        public SizeController(ILogger<SizeController> logger)
        {
            _logger = logger;
            sizeServices = new SizeServices();
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            //lấy product tywf db dựa theo id truyền vào route
            Size p = sizeServices.GetSizeById(id);
            return View(p);
        }
        public IActionResult Edit(Size p)
        {
            if (sizeServices.CreateSize(p))
            {
                return RedirectToAction("ShowlistSize");
            }
            else return BadRequest();
        }
        public IActionResult Create()//hiển thị
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Size p)//thực hiện tạo mới
        {
            if (sizeServices.CreateSize(p))
            {
                return RedirectToAction("ShowlistSize");
            }
            else return BadRequest();
        }
        public IActionResult Delete(Guid id)
        {
            if (sizeServices.DeleteSize(id))
            {
                return RedirectToAction("ShowlistSize");
            }
            else return BadRequest();
        }

        public IActionResult ShowlistSize()
        {
            //using (MixiDbContext c = new MixiDbContext())
            //{
            //    var lists = c.Products.Include("Size").Include("Color").ToList();
            //    return View(lists);
            //}
            List<Size> sizes = sizeServices.GetAllSize();
            return View(sizes);
        }

        public IActionResult Details(Guid id)
        {
            MixiDbContext dbContext = new MixiDbContext();
            var size = dbContext.Sizes.Find(id);
            return View(size);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
