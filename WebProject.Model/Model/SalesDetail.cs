using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebProject.Model.Model
{
   public class SalesDetail
    {
        public int Id { get; set; }
        
        public int Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal TotalMRP { get; set; }

        public int SalesMasterId { get; set; }
        public SalesMaster SalesMaster { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
