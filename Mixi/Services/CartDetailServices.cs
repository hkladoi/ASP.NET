using Microsoft.EntityFrameworkCore;
using Mixi.IServices;
using Mixi.Models;

namespace Mixi.Services
{
    public class CartDetailServices : ICartDetailServices
    {
        MixiDbContext _dbContext;
        public CartDetailServices()
        {
            _dbContext = new MixiDbContext();
        }
        public bool CreateCartDetail(CartDetail cd)
        {
            try
            {
                _dbContext.CartDetails.Add(cd);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteCartDetail(Guid id)
        {
            try
            {
                var cartDetail = _dbContext.CartDetails.Find(id);
                _dbContext.CartDetails.Remove(cartDetail);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<CartDetail> GetAllCartDetail()
        {
            return _dbContext.CartDetails.Include(cd => cd.Product).ThenInclude(p => p.Color)
                .Include(cd => cd.Product).ThenInclude(p => p.Size)
                .Include(cd => cd.Product).ThenInclude(p => p.Images)
                .Include(cd => cd.Product).ThenInclude(p => p.Category)
                .ToList();
        }

        public CartDetail GetCartDetailById(Guid id)
        {
            return _dbContext.CartDetails.FirstOrDefault(c => c.CartID == id);
        }

        public List<CartDetail> GetCartDetailByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCartDetail(CartDetail cd)
        {
            try
            {
                var cartDetail = _dbContext.CartDetails.Find(cd.CartID);
                cartDetail.ProductID = cd.ProductID;
                cartDetail.CartID = cd.CartID;
                cartDetail.Quantity = cd.Quantity;
                _dbContext.CartDetails.Update(cartDetail);
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
