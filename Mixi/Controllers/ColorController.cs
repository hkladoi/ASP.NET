using Microsoft.AspNetCore.Mvc;
using Mixi.Models;
using System.Diagnostics;
using Mixi.Services;
using Mixi.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Mixi.Controllers
{
    public class ColorController : Controller
    {
        private readonly ILogger<ColorController> _logger;
        private readonly IColorServices colorServices;
        public ColorController(ILogger<ColorController> logger)
        {
            _logger = logger;
            colorServices = new ColorServices();
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            //lấy product tywf db dựa theo id truyền vào route
            Color p = colorServices.GetColorById(id);
            return View(p);
        }
        public IActionResult Edit(Color p)
        {
            if (colorServices.UpdateColor(p))
            {
                return RedirectToAction("ShowlistColor");
            }
            else return BadRequest();
        }
        public IActionResult Create()//hiển thị
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Color p)//thực hiện tạo mới
        {
            if (colorServices.CreateColor(p))
            {
                return RedirectToAction("ShowlistColor");
            }
            else return BadRequest();
        }
        public IActionResult Delete(Guid id)
        {
            if (colorServices.DeleteColor(id))
            {
                return RedirectToAction("ShowlistColor");
            }
            else return BadRequest();
        }

        public IActionResult ShowlistColor()
        {
            //using (MixiDbContext c = new MixiDbContext())
            //{
            //    var lists = c.Products.Include("Size").Include("Color").ToList();
            //    return View(lists);
            //}
            List<Color> colors = colorServices.GetAllColor();
            return View(colors);
        }

        public IActionResult Details(Guid id)
        {
            MixiDbContext dbContext = new MixiDbContext();
            var color = dbContext.Colors.Find(id);
            return View(color);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
