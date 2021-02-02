using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.ComponentModel.DataAnnotations;

using WebProject.Model.Model;
namespace WebProject.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        
        //[Display(Name = "Category Code:")]
        //public String CategoryCode
        //{
        //    set;
        //    get;
        //}
        [Required(ErrorMessage = "Code Can Not be Empty")]
        public String Code
        {
            set;
            get;
        }
        [Required(ErrorMessage = "Category Can Not be Empty")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name Can Not be Empty")]
        public String Name { set; get; }

        [Required(ErrorMessage = "Recorder_Level Can Not be Empty")]
        public int Recorder_Level
        {
            set;
            get;
        }
        public String Description
        {
            set;
            get;
        }
        public List<Product> products { set; get; }
        public List<SelectListItem> CategorySelectListItems { get; set; }
    }
}