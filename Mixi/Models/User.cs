namespace Mixi.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Account { get; set; }
        public int Status { get; set; }

        public virtual Role Roles { get; set; }
        public virtual List<Bill> bill { get; set; }
        public virtual Cart Carts { get; set; }
    }
}
