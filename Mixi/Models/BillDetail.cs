namespace Mixi.Models
{
    public class BillDetail
    {
        public Guid ID { get; set; }
        public Guid BillID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual Bill bill { get; set; }
        public virtual Product product { get; set; }
    }
}
