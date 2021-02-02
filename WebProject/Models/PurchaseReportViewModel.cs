using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProject.Model.Model;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class PurchaseReportViewModel
    {
        public PurchaseReportViewModel()
        {
            PurchaseReport = new List<PurchaseReportViewModel>();
        }
        public DateTime Date { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public string Category { set; get; }
        public int Availableqty { set; get; }
        public decimal CP { set; get; }
        public decimal MRP { set; get; }
        public decimal Profit { set; get; }
        public List<PurchaseReportViewModel> PurchaseReport { set; get; }
    }
}