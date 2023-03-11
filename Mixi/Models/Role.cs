namespace Mixi.Models
{
    public class Role
    {
        public Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public int Status { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
