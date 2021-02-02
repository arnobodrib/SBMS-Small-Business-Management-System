using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using WebProject.Model.Model;
using WebProject.Models;

namespace WebProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CategoryViewModel, Category>();
                cfg.CreateMap<Category, CategoryViewModel>();


                cfg.CreateMap<CustomerViewModel, Customer>();
                cfg.CreateMap<Customer, CustomerViewModel>();

                cfg.CreateMap<SalesViewModel, SalesMaster>();
                cfg.CreateMap<SalesMaster, SalesViewModel>();

                cfg.CreateMap<SalesViewModel, SalesDetail>();
                cfg.CreateMap<SalesDetail, SalesViewModel>();

                cfg.CreateMap<SupplierViewModel, Supplier>();
                cfg.CreateMap<Supplier, SupplierViewModel>();

                cfg.CreateMap<Purchase, PurchaseViewModel>();
                cfg.CreateMap<PurchaseViewModel, Purchase>();

                cfg.CreateMap<PurchaseDetails, PurchaseViewModel>();
                cfg.CreateMap<PurchaseViewModel, PurchaseDetails>();
            });
        }
    }
}
