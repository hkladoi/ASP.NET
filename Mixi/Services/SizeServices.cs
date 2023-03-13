using Mixi.IServices;
using Mixi.Models;

namespace Mixi.Services
{
    public class SizeServices : ISizeServices
    {
        MixiDbContext _dbContext;
        public SizeServices()
        {
            _dbContext = new MixiDbContext();
        }
        public bool CreateSize(Size s)
        {
            try
            {
                _dbContext.Sizes.Add(s);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteSize(Guid id)
        {
            try
            {
                var size = _dbContext.Sizes.Find(id);
                _dbContext.Sizes.Remove(size);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Size> GetAllSize()
        {
            return _dbContext.Sizes.ToList();
        }

        public Size GetSizeById(Guid id)
        {
            return _dbContext.Sizes.FirstOrDefault(c => c.SizeID == id);
        }

        public List<Size> GetSizeByName(string name)
        {
            return _dbContext.Sizes.Where(c => c.Name.Contains(name)).ToList();
        }

        public bool UpdateSize(Size s)
        {
            try
            {
                var size = _dbContext.Sizes.Find(s.SizeID);
                size.Name = s.Name;
                size.Status = s.Status;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
