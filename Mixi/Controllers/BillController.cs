using Mixi.Services;
using Mixi.IServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mixi.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mixi.Controllers
{
    public class BillController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserServices userServices;
        private readonly ICartDetailServices cartDetailServices;
        private readonly IProductServices productServices;
        private readonly IBillServices billServices;
        private readonly IBillDetailServices billDetailServices;

        public BillController(ILogger<AccountController> logger)
        {
            _logger = logger;
            userServices = new UserServices();
            cartDetailServices = new CartDetailServices();
            productServices = new ProductServices();
            billServices = new BillServices();
            billDetailServices = new BillDetailServices();
        }
        public IActionResult Pay(string name, string phone, string address, string description)
        {
            var UserID = userServices.GetUserByName(HttpContext.Session.GetString("acc"))[0].UserID;
            var listCartDetails = cartDetailServices.GetAllCartDetail().Where(c => c.UserID == UserID);
            var Chuoi = "";
            var outOfStockProducts = listCartDetails
                             .Where(item => item.Quantity > productServices.GetProductById(item.ProductID).AvailableQuantity)
                             .Select(item => '"' + productServices.GetProductById(item.ProductID).Name + " chỉ còn " + productServices.GetProductById(item.ProductID).AvailableQuantity + '"');
            Chuoi = string.Join(" ", outOfStockProducts);

            if (listCartDetails.Any())
            {
                if (Chuoi == "")
                {
                    var bill = new Bill()
                    {
                        BillID = new Guid(),
                        CreateDate = DateTime.Now,
                        UserID = UserID,
                        Name = name,
                        Phone = phone,
                        Address = address,
                        Description = description,
                        Status = 0,
                        TotalPrice = listCartDetails.Sum(x => x.Product.SalePrice > 0 ? (x.Product.Price - x.Product.SalePrice) * x.Quantity : x.Product.Price * x.Quantity)
                    };
                    billServices.CreateBill(bill);
                    foreach (var item in listCartDetails)
                    {
                        billDetailServices.CreateBillDetail(new BillDetail()
                        {
                            ID = new Guid(),
                            BillID = bill.BillID,
                            ProductID = item.ProductID,
                            Quantity = item.Quantity,
                            Price = item.Product.Price
                        });
                        cartDetailServices.DeleteCartDetail(item.CartID);
                        var product = productServices.GetProductById(item.ProductID);
                        product.AvailableQuantity -= item.Quantity;
                        productServices.UpdateProduct(product);
                    }
                }
                else
                {
                    TempData["QuantityError"] = "số lượng tồn của sản phẩm " + Chuoi;
                }
            }
            else
            {
                TempData["PayError"] = "Thanh toán không thành công";
            }
            HttpContext.Session.Remove("itemCount");
            return RedirectToAction("ShowCartUser", "Cart");
        }
        public IActionResult BillManager()
        {
            List<Bill> billList = billServices.GetAllBill();
            return View(billList);
        }
        public IActionResult BillManagerUser()
        {
            var UserID = userServices.GetUserByName(HttpContext.Session.GetString("acc"))[0].UserID;
            List<Bill> billList = billServices.GetAllBill().Where(c => c.UserID == UserID).ToList();
            return View(billList);
        }
        public IActionResult BillDetailsManager(Guid id)
        {
            List<BillDetail> bills = billDetailServices.GetAllBillDetail().Where(c => c.BillID == id).ToList();
            return View(bills);
        }
    }
}
