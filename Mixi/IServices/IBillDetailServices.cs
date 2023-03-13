using Mixi.Models;

namespace Mixi.IServices
{
    public interface IBillDetailServices
    {
        public bool CreateBillDetail(BillDetail bd);
        public bool UpdateBillDetail(BillDetail bd);
        public bool DeleteBillDetail(Guid id);
        public List<BillDetail> GetAllBillDetail();
        public BillDetail GetBillDetailById(Guid id);
        public List<BillDetail> GetBillDetailByName(string name);
    }
}
