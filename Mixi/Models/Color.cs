namespace Mixi.Models
{
    public class Color
    {
        public Guid ColorID { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
