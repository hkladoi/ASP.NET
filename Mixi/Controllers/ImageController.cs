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
        public IActionResult Create(Image p, List<IFormFile> formFile)//thực hiện tạo mới
        {
            int i = 0;
            foreach (var file in formFile)
            {
                if (file != null && file.Length > 0)
                {
                    var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot", "Image", file.FileName
                    );
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    switch (i)
                    {
                        case 0:
                            p.LinkImage = file.FileName;
                            break;
                        case 1:
                            p.LinkImage1 = file.FileName;
                            break;
                        case 2:
                            p.LinkImage2 = file.FileName;
                            break;
                        case 3:
                            p.LinkImage3 = file.FileName;
                            break;
                        case 4:
                            p.LinkImage4 = file.FileName;
                            break;
                        default:
                            break;
                    }
                    i++;
                }
            }



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
