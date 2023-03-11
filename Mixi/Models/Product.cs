namespace Mixi.Models
{
    public class Product
    {
        public Guid ProductID { get; set; }
        public Guid ColorID { get; set; }
        public Guid SizeID { get; set; }
        public Guid CategoryID { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public decimal SalePrice { get; set; }
        public int AvailableQuantity { get; set; }
        public int Status { get; set; }
        public string Supplier { get; set; }
        public string Description { get; set; }
        public virtual List<BillDetail> BillDetails { get; set; }
        public virtual List<CartDetail> CartDetails { get; set; }
        public virtual Size Size { get; set; }
        public virtual Color Color { get; set; }
        public virtual Category Category { get; set; }
        public virtual List<Image> Images { get; set; }

    }
}
