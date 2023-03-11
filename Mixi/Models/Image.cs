namespace Mixi.Models
{
    public class Image
    {
        public Guid ImageID { get; set; }
        public Guid ProductID { get; set; }
        public string Name { get; set; }
        public string LinkImage { get; set; }
        public virtual Product Product { get; set; }
    }
}
