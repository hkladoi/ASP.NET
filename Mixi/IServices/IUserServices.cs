using Mixi.Models;
namespace Mixi.IServices
{
    public interface IUserServices
    {
        public bool CreateUser(User u);
        public bool UpdateUser(User u);
        public bool DeleteUser(Guid id);
        public List<User> GetAllUser();
        public User GetUserById(Guid id);
        public List<User> GetUserByName(string name);
    }
}
