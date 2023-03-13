using Mixi.IServices;
using Mixi.Models;

namespace Mixi.Services
{
    public class ColorServices : IColorServices
    {
        MixiDbContext _dbContext;
        public ColorServices()
        {
            _dbContext = new MixiDbContext();
        }
        public bool CreateColor(Color co)
        {
            try
            {
                _dbContext.Colors.Add(co);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteColor(Guid id)
        {
            try
            {
                var color = _dbContext.Colors.Find(id);
                _dbContext.Colors.Remove(color);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Color> GetAllColor()
        {
            return _dbContext.Colors.ToList();
        }

        public Color GetColorById(Guid id)
        {
            return _dbContext.Colors.FirstOrDefault(c => c.ColorID == id);
        }

        public List<Color> GetColorByName(string name)
        {
            return _dbContext.Colors.Where(c => c.Name.Contains(name)).ToList();
        }

        public bool UpdateColor(Color co)
        {
            try
            {
                var color = _dbContext.Colors.Find(co.ColorID);
                color.Name= co.Name;
                color.Status= co.Status;
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
