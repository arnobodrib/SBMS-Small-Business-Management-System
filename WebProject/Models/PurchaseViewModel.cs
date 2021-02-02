using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using WebProject.Model.Model;

namespace WebProject.Models
{
    public class PurchaseViewModel
    {

        //-----------Purchase
        public int Id { get; set; }

        public string ItemSearch { get; set; }

        [Required(ErrorMessage = "InvoiceNo is required")]
        //[StringLength(4, MinimumLength = 4, ErrorMessage = "Code length must be exact 4")]
        public string InvoiceNo { get; set; }

        [Required(ErrorMessage = "Date is required")]
        //[StringLength(4, MinimumLength = 4, ErrorMessage = "code length must be exact 4")]
        public DateTime Date { get; set; }

        public int SupplierId { get; set; }

        public int CategoryId { get; set; }

        public int ProductId { get; set; }


        public virtual List<Purchase> Purchases { set; get; }
        public virtual List<PurchaseDetails> PurchaseDetails { get; set; }

        public virtual PurchaseDetails PurchaseDetail { get; set; }
        public virtual Product ProductDetail { get; set; }
        public virtual Category CategoryDetail { get; set; }
        public virtual Supplier SupplierDetail { get; set; }
        public virtual Purchase Purchase { get; set; }


        public List<SelectListItem> SupplierSelectListItems { get; set; }
        public List<SelectListItem> CategorySelectListItems { get; set; }
        public List<SelectListItem> ProductSelectListItems { get; set; }


    }
}