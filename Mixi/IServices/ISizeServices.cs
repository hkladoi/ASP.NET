using Mixi.Models;
namespace Mixi.IServices
{
    public interface ISizeServices
    {
        public bool CreateSize(Size s);
        public bool UpdateSize(Size s);
        public bool DeleteSize(Guid id);
        public List<Size> GetAllSize();
        public Size GetSizeById(Guid id);
        public List<Size> GetSizeByName(string name);
    }
}
