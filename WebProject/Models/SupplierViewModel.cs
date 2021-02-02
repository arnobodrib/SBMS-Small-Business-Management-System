using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProject.Model.Model;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace WebProject.Models
{
    public class SupplierViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Code length must be exact 4")]
        //[MaxLength(4,ErrorMessage ="code can not be more than 4")]
        [Display(Name = "Supplier Code:")]
        public string Code { get; set; }



        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Supplier Name:")]
        public string Name { get; set; }




        [Display(Name = "Address:")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Email is Required")]
        [Display(Name = "Email:")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Contact is required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Number Must be in 11")]
        [Display(Name = "Contact:")]
        public string Contact { get; set; }

        
        [Display(Name = "Contact Person:")]
        public  string ContactPerson { get; set; }


        public List<Supplier> Suppliers { get; set; }

    }
}