namespace Mixi.Models
{
    public class Category
    {
        public Guid CategoryID { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
