using Mixi.Models;
namespace Mixi.IServices
{
    public interface IColorServices
    {
        public bool CreateColor(Color co);
        public bool UpdateColor(Color co);
        public bool DeleteColor(Guid id);
        public List<Color> GetAllColor();
        public Color GetColorById(Guid id);
        public List<Color> GetColorByName(string name);
    }
}
