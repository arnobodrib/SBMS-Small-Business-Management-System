using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProject.Model.Model;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="code is required")]
        [StringLength(4,MinimumLength =4,ErrorMessage ="code length must be exact 4")]
        [Display(Name ="Category Code:")]
        public string Code { get; set; }
        [Required(ErrorMessage ="Name is required")]
        [Display(Name = "Category Name:")]
        public string Name { get; set; }

        public List<Category> Categories{ get; set; }
    }
}