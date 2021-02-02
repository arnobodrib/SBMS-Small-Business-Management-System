using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProject.Model.Model;
using WebProject.Models;
using WebProject.BLL.BLL;
using AutoMapper;

namespace WebProject.Controllers
{
    public class SalesController : Controller
    {

        CustomerManager _customerManager = new CustomerManager();
        CategoryManager _categoryManager = new CategoryManager();
        ProductManager _productManager = new ProductManager();
        SalesManager _salesManager = new SalesManager();
       
        [HttpGet]
        public ActionResult Add()
        {
            SalesViewModel salesViewModel = new SalesViewModel();

            salesViewModel.CustomerSelectListItems = _customerManager.GetAll()
                                                                        .Select(c => new SelectListItem()
                                                                        {
                                                                            Value = c.Id.ToString(),
                                                                            Text = c.Name
                                                                        }).ToList();
            salesViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                                        .Select(c => new SelectListItem()
                                                                        {
                                                                            Value = c.Id.ToString(),
                                                                            Text = c.Name
                                                                        }).ToList();

        
            return View(salesViewModel);
        }

        [HttpPost]
        public ActionResult Add(SalesViewModel salesViewModel)
        {
            string message = "";

            if (ModelState.IsValid)
            {
                var resetLoyaltyPoint = salesViewModel.LoyaltyPoint - (salesViewModel.LoyaltyPoint / 10);
                var newLoyaltyPoint = Convert.ToInt32(resetLoyaltyPoint + ((salesViewModel.GrandTotal) / 1000));
                var customer = _customerManager.GetById(salesViewModel.CustomerId);
                customer.LoyaltyPoint = newLoyaltyPoint;
                _customerManager.Update(customer);

                salesViewModel.SalesCode = _salesManager.GetSalesCode();
                SalesMaster salesMaster = Mapper.Map<SalesMaster>(salesViewModel);

                if (_salesManager.Add(salesMaster))
                {
                    message = "Sale Info Saved";
                }
                else
                {
                    message = "Sale Info not saved";
                }
            }
            else
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                message = "modelstate is invalid";
            }

            ViewBag.Message = message;

            ModelState.Clear();
            SalesViewModel emptySalesViewModel = new SalesViewModel();


            emptySalesViewModel.CustomerSelectListItems = _customerManager.GetAll()
                                                                         .Select(c => new SelectListItem()
                                                                         {
                                                                             Value = c.Id.ToString(),
                                                                             Text = c.Name
                                                                         }).ToList();
            emptySalesViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                                        .Select(c => new SelectListItem()
                                                                        {
                                                                            Value = c.Id.ToString(),
                                                                            Text = c.Name
                                                                        }).ToList();


            return View(emptySalesViewModel);
           
        }

        public JsonResult GetProductByCategoryId(int categoryId)
        {
            var productList = _productManager.GetAll().Where(c=>c.CategoryId== categoryId).ToList();
            var products = from p in productList select (new { p.Id, p.Name });
            return Json(products,JsonRequestBehavior.AllowGet);
        }
        //load loyalty point
        public JsonResult GetLPointByCustomerId(int customerId)
        {
            var Customer = _customerManager.GetById(customerId);
           
            return Json(Customer, JsonRequestBehavior.AllowGet);
        }

        //load qnt and mrp by product id
        public JsonResult GetQntNMRPByProductId(int productId)
        {
            var AvailableQnty = _salesManager.GetAvailableQntyByProductId(productId);
            var MRP = _salesManager.GetMRPByProductId(productId);
            var reorderLevel = _salesManager.GetReorderLevel(productId);
            var result = new { AvailableQnty = AvailableQnty, MRP = MRP, reorderLevel= reorderLevel };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //check reorder level
        public JsonResult GetReorderLevel(int productId)
        {
            var reorderLevel = _salesManager.GetReorderLevel(productId);
            return Json(reorderLevel, JsonRequestBehavior.AllowGet);
        }
        
        [HttpGet]
        public ActionResult SalesSearch()
        {
            List<SalesMaster> salesMasters = _salesManager.GetAllMasterDetails();
            List<SalesDetail> salesDetails = _salesManager.GetAllSaleDetails();
            List<Customer> customers = _customerManager.GetAll();
            List<Product> products = _productManager.GetAll();

            var searchResult = (from sm in salesMasters
                               //join sd in salesDetails on sm.Id equals sd.SalesMasterId
                               join c in customers on sm.CustomerId equals c.Id
                               //join p in products on sd.ProductId equals p.Id
                               orderby sm.Date descending
                                select new SalesViewModel
                               {
                                   SalesMasterId = sm.Id,
                                   SalesCode =sm.SalesCode,
                                   CustomerName=c.Name,
                                   Date=sm.Date,
                                   //ProductName=p.Name,
                                   //Quantity=sd.Quantity,
                                   GrandTotal=sm.GrandTotal,
                                   DiscountAmount=sm.DiscountAmount,
                                   PayableAmount=sm.PayableAmount
                               }).ToList();
            
            return View(searchResult);
        }
        [HttpPost]
        public ActionResult SalesSearch(DateTime StartDate,DateTime EndDate)
        {
            List<SalesMaster> salesMasters = _salesManager.GetAllMasterDetails();
            List<SalesDetail> salesDetails = _salesManager.GetAllSaleDetails();
            List<Customer> customers = _customerManager.GetAll();
            List<Product> products = _productManager.GetAll();
          
            var masterResult = (from sm in salesMasters
                                //join sd in salesDetails on sm.Id equals sd.SalesMasterId
                                join c in customers on sm.CustomerId equals c.Id
                                //join p in products on sd.ProductId equals p.Id
                                where sm.Date>=StartDate && sm.Date<=EndDate
                                orderby sm.Date descending
                                select new SalesViewModel
                                {
                                    SalesMasterId = sm.Id,
                                    SalesCode = sm.SalesCode,
                                    CustomerName = c.Name,
                                    Date = sm.Date,
                                    //ProductName = p.Name,
                                    //Quantity = sd.Quantity,
                                    GrandTotal = sm.GrandTotal,
                                    DiscountAmount = sm.DiscountAmount,
                                    PayableAmount = sm.PayableAmount
                                }).ToList();

            return View(masterResult);
        }

        [HttpGet]
        public ActionResult SalesDetails(int Id)
        {
            List<SalesMaster> salesMasters = _salesManager.GetAllMasterDetails();
            List<SalesDetail> salesDetails = _salesManager.GetAllSaleDetails();
            List<Customer> customers = _customerManager.GetAll();
            List<Product> products = _productManager.GetAll();

            var detailsResult = (from sm in salesMasters
                                 join sd in salesDetails on sm.Id equals sd.SalesMasterId
                                 join c in customers on sm.CustomerId equals c.Id
                                 join p in products on sd.ProductId equals p.Id
                                 where sd.SalesMasterId == Id
                                 orderby sm.Date descending
                                 select new SalesViewModel
                                 {
                                     SalesMasterId = sm.Id,
                                     SalesCode = sm.SalesCode,
                                     CustomerName = c.Name,
                                     Date = sm.Date,
                                     ProductName = p.Name,
                                     Quantity = sd.Quantity,
                                     MRP=sd.MRP,
                                     TotalMRP=sd.TotalMRP,
                                     GrandTotal = sm.GrandTotal,
                                     DiscountAmount = sm.DiscountAmount,
                                     PayableAmount = sm.PayableAmount
                                 }).ToList();
            return View(detailsResult);
        }   
           

    }
}