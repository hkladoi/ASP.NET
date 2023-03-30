using Mixi.Models;
using Newtonsoft.Json;

namespace Mixi.Services
{
    public static class SessionServices
    {
        public static List<Product> GetObjFomSession(ISession session, string key)
        {
            //lấy string
            var JsonData = session.GetString(key);
            if (JsonData == null) return new List<Product>();
            //chuyển dữ liệu
            var products = JsonConvert.DeserializeObject<List<Product>>(JsonData);
            return products;
        }
        public static void SetObjToSession(ISession session, string key, object value)
        {
            var JsonData = JsonConvert.SerializeObject(value);
            session.SetString(key, JsonData);
        }
        public static bool CheckObjInList(Guid id, List<Product> products)
        {
            return products.Any(p => p.ProductID == id);
        }
    }
}
