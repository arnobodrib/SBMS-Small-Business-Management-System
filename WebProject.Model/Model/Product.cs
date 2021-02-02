using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebProject.Model.Model
{
    public class Product
    {
        public int Id { get; set; }
        public String CategoryCode
        {
            set;
            get;
        }
        public int CategoryId { get; set; }
        public String Code
        {
            set;
            get;
        }
        public String Name
        {
            set;
            get;
        }
        //public Decimal UnitPrice
        //{
        //    set;
        //    get;
        //}

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
    }
}
