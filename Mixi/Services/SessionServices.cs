using Mixi.Models;
using Mixi.ViewModel;
using Newtonsoft.Json;

namespace Mixi.Services
{
    public static class SessionServices
    {
        public static List<CartViewModel> GetObjFomSession(ISession session, string key)
        {
            //lấy string
            var JsonData = session.GetString(key);
            if (JsonData == null) return new List<CartViewModel>();
            //chuyển dữ liệu
            var products = JsonConvert.DeserializeObject<List<CartViewModel>>(JsonData);
            return products;
        }
        public static void SetObjToSession(ISession session, string key, object value)
        {
            var JsonData = JsonConvert.SerializeObject(value);
            session.SetString(key, JsonData);
        }
        public static List<Product> GetObjFomSessionCart(ISession session, string key)
        {
            //lấy string
            var JsonData = session.GetString(key);
            if (JsonData == null) return new List<Product>();
            //chuyển dữ liệu
            var Product = JsonConvert.DeserializeObject<List<Product>>(JsonData);
            return Product;
        }
        public static void SetObjToSessionCart(ISession session, string key, object value)
        {
            var JsonData = JsonConvert.SerializeObject(value);
            session.SetString(key, JsonData);
        }
        public static bool CheckObjInList(Guid id, List<CartViewModel> products)
        {
            return products.Any(p => p.ProductID == id);
        }
        public static void GetItem(List<CartDetail> cartDetails, ISession session)
        {
            session.Remove("cartDetails");
            SetObjToSession(session, "cartDetails", cartDetails);
        }
        public static void RemoveSession(ISession session, string key)
        {
            session.Remove(key);
        }
    }
}
