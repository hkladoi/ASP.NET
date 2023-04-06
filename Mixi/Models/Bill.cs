namespace Mixi.Models
{
    public class Bill
    {
        public Guid BillID { get; set; }
        public string BillCode { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid UserID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public decimal TotalPrice { get; set; }
        public virtual IEnumerable<BillDetail> BillDetails { get; set; }
        public virtual User User { get; set; }
    }
}
