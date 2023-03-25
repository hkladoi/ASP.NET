using Mixi.Models;

namespace Mixi.ViewModel
{
    public class ProductCategoryViewModel
    {
        public string CategoryName { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
