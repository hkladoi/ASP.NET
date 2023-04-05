using Mixi.IServices;
using Mixi.Models;

namespace Mixi.Services
{
    public class BillServices : IBillServices
    {
        MixiDbContext _dbContext;
        public BillServices()
        {
            _dbContext = new MixiDbContext();
        }
        public string GenerateBilltCode()
        {
            if (_dbContext.Bills.Count() == 0)
            {
                return "PAY-1";
            }
            else return "PAY-" + _dbContext.Bills.Max(c => Convert.ToInt32(c.BillCode.Substring(4, c.BillCode.Length - 4)) + 1);
        }
        public bool CreateBill(Bill b)
        {
            try
            {
                b.BillCode = GenerateBilltCode();
                _dbContext.Bills.Add(b);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteBill(Guid id)
        {
            try
            {
                var Bill = _dbContext.Bills.Find(id);
                _dbContext.Bills.Remove(Bill);
                _dbContext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Bill> GetAllBill()
        {
            return _dbContext.Bills.ToList();
        }

        public Bill GetBillById(Guid id)
        {
            return _dbContext.Bills.FirstOrDefault(c => c.BillID == id);
        }

        public List<Bill> GetBillByName(string name)
        {
            return _dbContext.Bills.Where(c => c.Name.Contains(name)).ToList();
        }

        public bool UpdateBill(Bill b)
        {
            try
            {
                var Bill = _dbContext.Bills.Find(b.BillID);
                Bill.UserID = b.UserID;
                Bill.Name = b.Name;
                Bill.Phone = b.Phone;
                Bill.Address = b.Address;
                Bill.Description = b.Description;
                Bill.Status = b.Status;
                _dbContext.Bills.Update(Bill);
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
