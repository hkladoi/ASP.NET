namespace Shopping_App.Models
{
    public class User
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid role { get; set; }
        public int status { get; set; }

        public virtual Role Roles { get; set; }
        public virtual IEnumerable<Bill> bill { get; set; }
        public virtual IEnumerable<Cart> Carts { get; set; }

    }
}
