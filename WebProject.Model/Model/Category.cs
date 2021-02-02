using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebProject.Model.Model
{
    public class Category


    {


        public Category()
        {
            Products = new List<Product>();
        }


        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual List<Product> Products { get; set; }
    }
}
