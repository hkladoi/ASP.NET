using Microsoft.EntityFrameworkCore;
using Shopping_App.Models;
using System.Reflection;

namespace Shopping_App.Models
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext()
        {

        }
        public ShopDbContext(DbContextOptions options) : base(options)
        {
        }
        //Db set
        public DbSet<Product> products { get; set; }
        public DbSet<Bill> bills { get; set; }
        public DbSet<BillDetails> billDetails { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartDetail> cartDetails { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<User> users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-6BDFC9E\HKLADOI;Initial Catalog=khaidb;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
