using Mixi.Models;

namespace Mixi.IServices
{
    public interface ICartServices
    {
        public bool CreateCart(Cart c);
        public bool UpdateCart(Cart c);
        public bool DeleteCart(Guid id);
        public List<Cart> GetAllCart();
        public Cart GetCartById(Guid id);
        public List<Cart> GetCartByName(string name);

    }
}
