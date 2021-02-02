using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProject.Model.Model;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class StockViewModel
    {
        public StockViewModel()
        {
            StockReport = new List<StockViewModel>();
        }
        public DateTime PurchaseDate { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public string Category { set; get; }
        public double ReorderLevel { set; get; }
        public DateTime ExpireDate { set; get; }
        public double OpeningBalance { set; get; }
        public double Inqty { set; get; }
        public double Out { set; get; }
        public double ClosingBalance{ set; get; }
        public List<StockViewModel> StockReport { set; get; }
        public List<SelectListItem> CategorySelectListItems { get; set; }
        public List<SelectListItem> ProductSelectListItems { get; set; }

    }
}