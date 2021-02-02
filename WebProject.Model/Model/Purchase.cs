using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebProject.Model.Model
{
    public class Purchase
    {

        public int Id { get; set; }

        public string InvoiceNo { get; set; }

        public DateTime Date { get; set; }

        public int SupplierId { get; set; }

        public virtual List<PurchaseDetails> PurchaseDetails { get; set; }
    }

}
