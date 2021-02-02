using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProject.Model.Model;
using WebProject.BLL.BLL;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
        
{
    public class SalesViewModel
    {
        
        public string SalesCode { get; set; }
        public int SalesMasterId { get; set; }
        public DateTime Date { get; set; }
        public decimal GrandTotal { get; set; }
        public int Discount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal PayableAmount { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int Quantity { get; set; }
        public decimal MRP { get; set; }
        public decimal TotalMRP { get; set; }

        public int CategoryId { get; set; }
        [Display(Name ="Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int AvailableQuantity { get; set; }
        public int LoyaltyPoint { get; set; }
        public string CustomerName { get; set; }
        public string ProductName { get; set; }

        public List<SelectListItem> CustomerSelectListItems { get; set; }
        public List<SelectListItem> CategorySelectListItems { get; set; }
       

        public virtual List<SalesDetail> SalesDetails { get; set; }

    }
}