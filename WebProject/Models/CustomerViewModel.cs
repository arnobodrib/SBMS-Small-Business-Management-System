using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebProject.Model.Model;
using System.ComponentModel.DataAnnotations;

namespace WebProject.Models
{
    public class CustomerViewModel
    {
        public CustomerViewModel()
        {
            LoyaltyPoint = 0;
        }
        public int Id { get; set; }
        [Required(ErrorMessage = "code is required")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "code length must be exact 4")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [RegularExpression("^[_A-Za-z'`+-.]+([_A-Za-z0-9'+-.]+)*@([A-Za-z0-9-])+(\\.[A-Za-z0-9]+)*(\\.([A-Za-z]*){3,})$", ErrorMessage = "Enter proper email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Format not proper of email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contact is required")]
        public string Contact { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Enter valid Number")]
        public int LoyaltyPoint { get; set; }

        public List<Customer> Customers { get; set; }



    }
}