using Microsoft.EntityFrameworkCore;
using Mixi.IServices;
using Mixi.Models;

namespace Mixi.Services
{
    public class BillDetailServices : IBillDetailServices
    {
        MixiDbContext _dbContext;
        public BillDetailServices()
        {
            _dbContext = new MixiDbContext();
        }
        public bool CreateBillDetail(BillDetail bd)
        {
            try
            {
                _dbContext.BillDetails.Add(bd);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool DeleteBillDetail(Guid id)
        {
            try
            {
                var BillDetail = _dbContext.BillDetails.Find(id);
                _dbContext.BillDetails.Remove(BillDetail);
                _dbContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<BillDetail> GetAllBillDetail()
        {
            return _dbContext.BillDetails.Include(bd => bd.product).ThenInclude(c => c.Images).ToList();
        }

        public BillDetail GetBillDetailById(Guid id)
        {
            return _dbContext.BillDetails.FirstOrDefault(x => x.ID == id);
        }

        public List<BillDetail> GetBillDetailByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateBillDetail(BillDetail bd)
        {
            try
            {
                var BillDetail = _dbContext.BillDetails.Find(bd.ID);
                BillDetail.ProductID = bd.ProductID;
                BillDetail.BillID = bd.ID;
                BillDetail.Quantity = bd.Quantity;
                BillDetail.Price = bd.Price;
                _dbContext.BillDetails.Update(BillDetail);
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
