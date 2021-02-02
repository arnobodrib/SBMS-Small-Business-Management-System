using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Model.Model;
using WebProject.Repository.Repository;

namespace WebProject.BLL.BLL
{
    public class SalesManager
    {

        SalesRepository _salesRepository = new SalesRepository();

        public string GetSalesCode()
        {
            return _salesRepository.GetSalesCode();
        }
        public int GetAvailableQntyByProductId(int productId)
        {
            return _salesRepository.GetAvailableQntyByProductId(productId);
        }
        public decimal GetMRPByProductId(int productId)
        {
            return _salesRepository.GetMRPByProductId(productId);
        }

        public int GetReorderLevel(int productId)
        {
            return _salesRepository.GetReorderLevel(productId);
        }

        public bool Add(SalesMaster salesMaster)
        {
            return _salesRepository.Add(salesMaster);
        }
        public List<SalesMaster> GetAllMasterDetails()
        {
            return _salesRepository.GetAllMasterDetails();
        }

        public List<SalesDetail> GetAllSaleDetails()
        {
            return _salesRepository.GetAllSaleDetails();
        }
    }
}
