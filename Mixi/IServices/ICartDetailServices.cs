using Mixi.Models;
namespace Mixi.IServices
{
    public interface ICartDetailServices
    {
        public bool CreateCartDetail(CartDetail cd);
        public bool UpdateCartDetail(CartDetail cd);
        public bool DeleteCartDetail(Guid id);
        public List<CartDetail> GetAllCartDetail();
        public CartDetail GetCartDetailById(Guid id);
        public List<CartDetail> GetCartDetailByName(string name);
    }
}
