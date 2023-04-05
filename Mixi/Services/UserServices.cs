using Mixi.IServices;
using Mixi.Models;

namespace Mixi.Services
{
    public class UserServices : IUserServices
    {
        MixiDbContext _dbContext;
        public UserServices()
        {
            _dbContext = new MixiDbContext();
        }

        public bool CreateUser(User u)
        {
            try
            {
                _dbContext.Users.Add(u);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool Createkach(User u)
        {

            try
            {
                var user = _dbContext.Users.Find(u.UserID);
                if (user.Email == u.Email)
                {
                    return false;
                }
                else if (user.Account == u.Account)
                {
                    return false;
                }
                else
                {
                    u.RoleID = Guid.Parse("6c54da06-1a6e-4404-8fe3-08db2ee1349c");
                    _dbContext.Users.Add(u);
                    _dbContext.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }
        public bool DeleteUser(Guid id)
        {
            try
            {
                var user = _dbContext.Users.Find(id);
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<User> GetAllUser()
        {
            return _dbContext.Users.ToList();
        }

        public User GetUserById(Guid id)
        {
            return _dbContext.Users.FirstOrDefault(c => c.UserID == id);
        }

        public List<User> GetUserByName(string name)
        {
            return _dbContext.Users.Where(c => c.Account.Contains(name)).ToList();
        }

        public bool UpdateUser(User u)
        {
            try
            {
                var user = _dbContext.Users.Find(u.UserID);
                user.RoleID = u.RoleID;
                user.FirstName = u.FirstName;
                user.LastName = u.LastName;
                user.PhoneNumber = u.PhoneNumber;
                user.Email = u.Email;
                user.Account = u.Account;
                user.Password = u.Password;
                user.Address = u.Address;
                user.Status = u.Status;
                _dbContext.Users.Update(user);
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
