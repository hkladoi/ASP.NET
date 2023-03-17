namespace Mixi.Models
{
    public class Image
    {
        public Guid ImageID { get; set; }
        public string Name { get; set; }
        public string LinkImage { get; set; }
        public string LinkImage1 { get; set; }
        public string LinkImage2 { get; set; }
        public string LinkImage3 { get; set; }
        public string LinkImage4 { get; set; }
        public virtual List<Product> Products { get; set; }
    }
}
