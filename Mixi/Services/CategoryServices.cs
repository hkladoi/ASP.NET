using Mixi.IServices;
using Mixi.Models;

namespace Mixi.Services
{
    public class CategoryServices : ICategoryServices
    {
        MixiDbContext _dbContext;
        public CategoryServices()
        {
            _dbContext = new MixiDbContext();
        }
        public bool CreateCategory(Category ca)
        {
            try
            {
                _dbContext.Categories.Add(ca);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteCategory(Guid id)
        {
            try
            {
                var category = _dbContext.Categories.Find(id);
                _dbContext.Categories.Remove(category);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Category> GetAllCategory()
        {
            return _dbContext.Categories.ToList();
        }

        public Category GetCategoryById(Guid id)
        {
            return _dbContext.Categories.FirstOrDefault(c => c.CategoryID == id);
        }

        public List<Category> GetCategoryByName(string name)
        {
            return _dbContext.Categories.Where(c => c.Name.Contains(name)).ToList();
        }

        public bool UpdateCategory(Category ca)
        {
            try
            {
                var category = _dbContext.Categories.Find(ca.CategoryID);
                category.Name = ca.Name;
                category.Status = ca.Status;
                _dbContext.Categories.Update(category);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
