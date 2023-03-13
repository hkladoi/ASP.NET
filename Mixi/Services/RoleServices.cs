using Mixi.IServices;
using Mixi.Models;

namespace Mixi.Services
{
    public class RoleServices : IRoleServices
    {
        MixiDbContext _dbContext;
        public RoleServices()
        {
            _dbContext = new MixiDbContext();
        }
        public bool CreateRole(Role r)
        {
            try
            {
                _dbContext.Roles.Add(r);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteRole(Guid id)
        {
            try
            {
                var role = _dbContext.Roles.Find(id);
                _dbContext.Roles.Remove(role);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Role> GetAllRole()
        {
            return _dbContext.Roles.ToList();
        }

        public Role GetRoleById(Guid id)
        {
            return _dbContext.Roles.FirstOrDefault(c => c.RoleID == id);
        }

        public List<Role> GetRoleByName(string name)
        {
            return _dbContext.Roles.Where(c => c.RoleName.Contains(name)).ToList();
        }

        public bool UpdateRole(Role r)
        {
            try
            {
                var role = _dbContext.Roles.Find(r.RoleID);
                role.RoleName = r.RoleName;
                role.Status = r.Status;
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
