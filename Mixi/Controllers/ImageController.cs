using Mixi.Services;
using Mixi.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mixi.Models;
using System.Diagnostics;

namespace Mixi.Controllers
{
    public class ImageController : Controller
    {
        private readonly ILogger<ImageController> _logger;
        private readonly IImageServices imageServices;
        public ImageController(ILogger<ImageController> logger)
        {
            _logger = logger;
            imageServices = new ImageServices();
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            //lấy product tywf db dựa theo id truyền vào route
            Image p = imageServices.GetImageById(id);
            return View(p);
        }
        public IActionResult Edit(Image p)
        {
            if (imageServices.UpdateImage(p))
            {
                return RedirectToAction("ShowlistImage");
            }
            else return BadRequest();
        }
        public IActionResult Create()//hiển thị
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Image p)//thực hiện tạo mới
        {
            if (imageServices.CreateImage(p))
            {
                return RedirectToAction("ShowlistImage");
            }
            else return BadRequest();
        }
        public IActionResult Delete(Guid id)
        {
            if (imageServices.DeleteImage(id))
            {
                return RedirectToAction("ShowlistImage");
            }
            else return BadRequest();
        }

        public IActionResult ShowlistImage()
        {
            //using (MixiDbContext c = new MixiDbContext())
            //{
            //    var lists = c.Products.Include("Size").Include("Color").ToList();
            //    return View(lists);
            //}
            List<Image> images = imageServices.GetAllImage();
            return View(images);
        }

        public IActionResult Details(Guid id)
        {
            MixiDbContext dbContext = new MixiDbContext();
            var image = dbContext.Images.Find(id);
            return View(image);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
