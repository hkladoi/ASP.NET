using Mixi.Models;
namespace Mixi.IServices
{
    public interface IImageServices
    {
        public bool CreateImage(Image i);
        public bool UpdateImage(Image i);
        public bool DeleteImage(Guid id);
        public List<Image> GetAllImage();
        public Image GetImageById(Guid id);
        public List<Image> GetImageByName(string name);
    }
}
