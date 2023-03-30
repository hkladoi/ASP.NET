using Mixi.Services;
using Mixi.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mixi.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Mixi.Controllers
{
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IRoleServices roleServices;
        public RoleController(ILogger<RoleController> logger)
        {
            _logger = logger;
            roleServices = new RoleServices();
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            //lấy product tywf db dựa theo id truyền vào route
            Role p = roleServices.GetRoleById(id);
            return View(p);
        }
        public IActionResult Edit(Role p)
        {
            if (roleServices.UpdateRole(p))
            {
                return RedirectToAction("ShowlistRole");
            }
            else return BadRequest();
        }
        public IActionResult Create()//hiển thị
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Role p)//thực hiện tạo mới
        {
            if (roleServices.CreateRole(p))
            {
                return RedirectToAction("ShowlistRole");
            }
            else return BadRequest();
        }
        public IActionResult Delete(Guid id)
        {
            if (roleServices.DeleteRole(id))
            {
                return RedirectToAction("ShowlistRole");
            }
            else return BadRequest();
        }

        public IActionResult ShowlistRole()
        {
            //using (MixiDbContext c = new MixiDbContext())
            //{
            //    var lists = c.Products.Include("Size").Include("Color").ToList();
            //    return View(lists);
            //}
            List<Role> roles = roleServices.GetAllRole();
            return View(roles);
        }

        public IActionResult Details(Guid id)
        {
            MixiDbContext dbContext = new MixiDbContext();
            var role = dbContext.Roles.Find(id);
            return View(role);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
