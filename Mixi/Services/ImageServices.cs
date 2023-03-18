using Mixi.IServices;
using Mixi.Models;

namespace Mixi.Services
{
    public class ImageServices : IImageServices
    {
        MixiDbContext _dbContext;
        public ImageServices()
        {
            _dbContext = new MixiDbContext();
        }
        public bool CreateImage(Image i)
        {
            try
            {
                _dbContext.Images.Add(i);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteImage(Guid id)
        {
            try
            {
                var image = _dbContext.Images.Find(id);
                _dbContext.Images.Remove(image);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Image> GetAllImage()
        {
            return _dbContext.Images.ToList();
        }

        public Image GetImageById(Guid id)
        {
            return _dbContext.Images.FirstOrDefault(c => c.ImageID == id);
        }

        public List<Image> GetImageByName(string name)
        {
            return _dbContext.Images.Where(c => c.Name.Contains(name)).ToList();
        }

        public bool UpdateImage(Image i)
        {
            try
            {
                var image = _dbContext.Images.Find(i.ImageID);
                image.Name = i.Name;
                image.LinkImage = i.LinkImage;
                image.LinkImage1 = i.LinkImage1;
                image.LinkImage1 = i.LinkImage2;
                image.LinkImage1 = i.LinkImage3;
                image.LinkImage1 = i.LinkImage4;
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
