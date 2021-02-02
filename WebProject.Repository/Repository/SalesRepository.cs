using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebProject.Model.Model;
using WebProject.DatabaseContext.DatabaseContext;

namespace WebProject.Repository.Repository
{
    public class SalesRepository
    {
        ProjectDbContext _dbContext = new ProjectDbContext();

        public string GetSalesCode()
        {
            var MaxSalesId = _dbContext.SalesMasters.Max(c=> (int?) c.Id) ?? 0;
            MaxSalesId = MaxSalesId + 1;
            string SalesId = MaxSalesId.ToString().PadLeft(4,'0');

            DateTime dt = DateTime.Now;
            string Year = dt.Year.ToString();

            string SalesCode = Year + "-" + SalesId;

            return SalesCode;
        }
        public int GetAvailableQntyByProductId(int productId)
        {
            var PurQnty =  _dbContext.PurchaseDetails.Where(x => x.ProductId == productId).Sum(x => (int?)x.Quantity) ?? 0;
            var SalesQnty = _dbContext.SalesDetails.Where(x => x.ProductId == productId).Sum(x => (int?)x.Quantity) ?? 0;
            var AvailableQnty = PurQnty - SalesQnty;

            return AvailableQnty;
        }
        public decimal GetMRPByProductId(int productId)
        {
            var PurDet = _dbContext.PurchaseDetails.Where(x => x.ProductId == productId).OrderByDescending(x => x.Id).FirstOrDefault();

            decimal PMrp = PurDet == null ? 0 : PurDet.MRP;

            //float PPMRP = float.Parse(PMrp.ToString());

            return PMrp;
        }

        public int GetReorderLevel(int productId)
        {
            var reorderLevel = _dbContext.Products.Where(x => x.Id == productId).FirstOrDefault().Recorder_Level;
            return reorderLevel;
        }

        public bool Add(SalesMaster salesMaster)
        {
            _dbContext.SalesMasters.Add(salesMaster);
            return _dbContext.SaveChanges() > 0;
        }

        public List<SalesMaster> GetAllMasterDetails()
        {
            return _dbContext.SalesMasters.ToList();
        }


        public List<SalesDetail> GetAllSaleDetails()
        {
            return _dbContext.SalesDetails.Include(c => c.SalesMaster).ToList();
        }


    }
}

