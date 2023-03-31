namespace Mixi.ViewModel
{
    public class CartViewModel
    {
        public Guid ProductID { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public string CategoryName { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public int AvailableQuantity { get; set; }
    }
}
