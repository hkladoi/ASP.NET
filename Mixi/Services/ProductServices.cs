using Microsoft.EntityFrameworkCore;
using Mixi.IServices;
using Mixi.Models;

namespace Mixi.Services
{
    public class ProductServices : IProductServices
    {
        MixiDbContext context;
        public ProductServices()
        {
            context = new MixiDbContext();
        }
        public bool CreateProduct(Product p)
        {
            try
            {
                p.ProductCode = GenerateProductCode();
                context.Products.Add(p);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public string GenerateProductCode()
        {
            int nextId = context.Products.Count() + 1;
            return "PRD-" + nextId.ToString();
        }

        public bool DeleteProduct(Guid id)
        {
            try
            {
                var product = context.Products.Find(id);
                context.Products.Remove(product);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Product> GetAllProduct()
        {
            return context.Products.ToList();
        }

        public Product GetProductById(Guid id)
        {
            return context.Products.FirstOrDefault(p => p.ProductID == id);
            //return context.Products.SingleOrDefault(p => p.ProductID == id);
        }

        public List<Product> GetProductByName(string name)
        {
            return context.Products.Where(p => p.Name.Contains(name)).ToList();
        }

        public bool UpdateProduct(Product p)
        {
            try
            {
                var product = context.Products.Find(p.ProductID);
                product.Name = p.Name;
                product.ProductCode = p.ProductCode;
                product.Price = p.Price;
                product.SalePrice = p.SalePrice;
                product.AvailableQuantity = p.AvailableQuantity;
                product.Status = p.Status;
                product.Description = p.Description;
                product.Supplier = p.Supplier;

                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
