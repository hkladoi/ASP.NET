namespace Shopping_App.Models
{
    public class BillDetails
    {
        public Guid ID { get; set; }
        public Guid IDHD { get; set; }
        public Guid IDSP { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public virtual Bill bill { get; set; }
        public virtual Product product { get; set; }
    }
}
