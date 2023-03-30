using Mixi.Models;

namespace Mixi.ViewModel
{
    public class ProductCategoryViewModel
    {
        public Guid ProductID { get; set; }
        public Guid ColorID { get; set; }
        public Guid SizeID { get; set; }
        public Guid CategoryID { get; set; }
        public Guid ImageID { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public int AvailableQuantity { get; set; }
        public int Status { get; set; }
        public string Supplier { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public string Image { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
