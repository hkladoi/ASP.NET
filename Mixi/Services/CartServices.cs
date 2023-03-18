using Mixi.IServices;
using Mixi.Models;

namespace Mixi.Services
{
    public class CartServices : ICartServices
    {
        MixiDbContext _dbContext;
        public CartServices()
        {
            _dbContext = new MixiDbContext();
        }
        public bool CreateCart(Cart c)
        {
            try
            {
                _dbContext.Carts.Add(c);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteCart(Guid id)
        {
            try
            {
                var cart = _dbContext.Carts.Find(id);
                _dbContext.Carts.Remove(cart);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Cart> GetAllCart()
        {
            return _dbContext.Carts.ToList();
        }

        public Cart GetCartById(Guid id)
        {
            return _dbContext.Carts.FirstOrDefault(c => c.UserID == id);
        }

        public List<Cart> GetCartByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCart(Cart c)
        {
            try
            {
                var cart = _dbContext.Carts.Find(c.UserID);
                cart.Description = c.Description;
                _dbContext.Carts.Update(cart);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
