using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebProject.Models
{
    public class PurchaseDetailsViewModel
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int SupplierId { get; set; }

        public int CategoryId { get; set; }

        public int ProductId { get; set; }
    }
}