using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using WebProject.Model.Model;
using WebProject.Models;
using WebProject.BLL.BLL;


namespace Error404.Controllers
{
    public class SalesReportController: Controller
    {

       
        
        PurchaseManager _purchaseManager = new PurchaseManager();
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();
        PurchaseDetails purchaseDetails = new PurchaseDetails();
        SalesManager _salesManager = new SalesManager();


        [HttpGet]
        public ActionResult Search()
        {
            SalesReportViewModel salesReportViewModel = new SalesReportViewModel();
            var purchases = _purchaseManager.GetAll();
            var products = _productManager.GetAll();
            var categories = _categoryManager.GetAll();
            var purchaseDetails = _purchaseManager.GetAllDetails();
            var salesDetails = _salesManager.GetAllSaleDetails();
            var report = (from sa in salesDetails
                          join Products in products on new { ProductId = sa.ProductId } equals new { ProductId = Products.Id } into Products_join
                          from Products in Products_join.DefaultIfEmpty()
                          join Categories in categories on new { CategoryId = Products.CategoryId } equals new { CategoryId = Categories.Id } into Categories_join
                          from Categories in Categories_join.DefaultIfEmpty()
                          group new { sa.SalesMaster, sa, Products, Categories } by new
                          {
                              sa.SalesMaster.Date,
                              sa.ProductId,
                              Products.Name,
                              Products.Code,
                              Column1 = Categories.Name
                          } into g
                          select new SalesReportViewModel
                          {
                              Date = g.Key.Date,
                              Code = g.Key.Code,
                              Name = g.Key.Name,
                              Category = g.Key.Column1,
                              Soldqnty = g.Sum(p => p.sa.Quantity),
                              CP = g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g1
                                select new
                                {
                                    Column1 =(g1.Sum(p => p.UnitPrice) /g.Count())
                                }).First().Column1),
                              SalesPrice = (g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g2
                                select new
                                {
                                    Column1 = (g2.Sum(p => p.MRP) / g.Count())
                                }).First().Column1)),
                              Profit = (g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g3
                                select new
                                {
                                    Column1 = (g3.Sum(p => p.MRP) / g.Count())
                                }).First().Column1) - g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g4
                                select new
                                {
                                    Column1 = (g4.Sum(p => p.UnitPrice) / g.Count())
                                }).First().Column1))
                          }).ToList();

            salesReportViewModel.SalesReport = report;

            return View(salesReportViewModel);
        }
       [HttpPost]
        public ActionResult Search(DateTime? startdate, DateTime? enddate)
        {
            SalesReportViewModel salesReportViewModel = new SalesReportViewModel();
            var purchases = _purchaseManager.GetAll();
            var products = _productManager.GetAll();
            var categories = _categoryManager.GetAll();
            var purchaseDetails = _purchaseManager.GetAllDetails();
            var salesDetails = _salesManager.GetAllSaleDetails();
            var report = (from sa in salesDetails
                          join Products in products on new { ProductId = sa.ProductId } equals new { ProductId = Products.Id } into Products_join
                          from Products in Products_join.DefaultIfEmpty()
                          join Categories in categories on new { CategoryId = Products.CategoryId } equals new { CategoryId = Categories.Id } into Categories_join
                          from Categories in Categories_join.DefaultIfEmpty()
                          group new { sa.SalesMaster, sa, Products, Categories } by new
                          {
                              sa.SalesMaster.Date,
                              sa.ProductId,
                              Products.Name,
                              Products.Code,
                              Column1 = Categories.Name
                          } into g
                          where g.Key.Date >=startdate && g.Key.Date<=enddate
                          select new SalesReportViewModel
                          {
                              Date = g.Key.Date,
                              Code = g.Key.Code,
                              Name = g.Key.Name,
                              Category = g.Key.Column1,
                              Soldqnty = g.Sum(p => p.sa.Quantity),
                              CP = g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g1
                                select new
                                {
                                    Column1 = (g1.Sum(p => p.UnitPrice) / g.Count())
                                }).First().Column1),
                              SalesPrice = (g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g2
                                select new
                                {
                                    Column1 = (g2.Sum(p => p.MRP) / g.Count())
                                }).First().Column1)),
                              Profit = (g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g3
                                select new
                                {
                                    Column1 = (g3.Sum(p => p.MRP) / g.Count())
                                }).First().Column1) - g.Sum(p => p.sa.Quantity) *
                              ((from Pu in purchaseDetails
                                where
                                 Pu.ProductId == g.Key.ProductId
                                group Pu by new
                                {
                                    Pu.ProductId
                                } into g4
                                select new
                                {
                                    Column1 = (g4.Sum(p => p.UnitPrice) / g.Count())
                                }).First().Column1))
                          }).ToList();

            salesReportViewModel.SalesReport = report;

            return View(salesReportViewModel);
        }

    }
}