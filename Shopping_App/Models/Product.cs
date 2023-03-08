namespace Shopping_App.Models
{
    public class Product
    {
        public Guid ID { get; set; }
        public string Name { get; set; }

        public int Price { get; set; }
        public int AvailableQuantity { get; set; }
        public int Status { get; set; }
        public string Supplier { get; set; }

        public string Description { get; set; }

        public virtual List<BillDetails> BillDetails { get; set; }
        public virtual List<CartDetail> CartDetails { get; set; }
    }
}
