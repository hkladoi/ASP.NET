using Mixi.Models;
namespace Mixi.IServices
{
    public interface ICategoryServices
    {
        public bool CreateCategory(Category ca);
        public bool UpdateCategory(Category ca);
        public bool DeleteCategory(Guid id);
        public List<Category> GetAllCategory();
        public Category GetCategoryById(Guid id);
        public List<Category> GetCategoryByName(string name);
    }
}
