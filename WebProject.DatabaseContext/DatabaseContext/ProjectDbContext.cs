using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WebProject.Model.Model;

namespace WebProject.DatabaseContext.DatabaseContext
{
    public class ProjectDbContext:DbContext
    {
        public ProjectDbContext()
        {
            Configuration.LazyLoadingEnabled = false; //Disable Lazy loading
        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Product> Products { set; get; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<SalesMaster> SalesMasters { get; set; }
        public DbSet<SalesDetail> SalesDetails { get; set; }

        public DbSet<Purchase> Purchases { set; get; }
        public DbSet<PurchaseDetails> PurchaseDetails { get; set; }


    }
}
