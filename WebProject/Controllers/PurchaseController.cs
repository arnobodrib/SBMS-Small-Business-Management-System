using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using WebProject.Model.Model;
using WebProject.BLL.BLL;

using WebProject.Models;

namespace WebProject.Controllers
{
    public class PurchaseController : Controller
    {
        PurchaseManager _purchaseManager = new PurchaseManager();
        SupplierManager _supplierManager = new SupplierManager();
        CategoryManager _categoryManager = new CategoryManager();
        ProductManager _productManager = new ProductManager();
        Purchase _purchase = new Purchase();
        List<PurchaseDetails> purchaseDetails = new List<PurchaseDetails>();

        [HttpGet]
        public ActionResult Add()
        {
            
            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            purchaseViewModel.PurchaseDetails = purchaseDetails;
            purchaseViewModel.SupplierSelectListItems = _supplierManager.GetAll()
                                                                        .Select(c=> new SelectListItem() 
                                                                        { 
                                                                            Value=c.Id.ToString(),Text=c.Name
                                                                        }).ToList();
            purchaseViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                                        .Select(c => new SelectListItem()
                                                                        {
                                                                            Value = c.Id.ToString(),
                                                                            Text = c.Name
                                                                        }).ToList();
            purchaseViewModel.ProductSelectListItems = _productManager.GetAll()
                                                                        .Select(c => new SelectListItem()
                                                                        {
                                                                            Value = c.Id.ToString(),
                                                                            Text = c.Name
                                                                        }).ToList();

            ViewBag.itemCategoryID = purchaseViewModel.CategorySelectListItems;
            ViewBag.itemProductID = purchaseViewModel.ProductSelectListItems;
            return View(purchaseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PurchaseViewModel purchaseViewModel)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                if (purchaseViewModel.PurchaseDetails != null)
                {

                    Purchase purchase = new Purchase();

                    purchase = Mapper.Map<Purchase>(purchaseViewModel);

                    _purchaseManager.Add(purchase);

                    message = "Purchased Successfully";

                }
                else
                {
                    message = "Please Add at least one product !";
                }
            }
            else
            {
                message = "Model State Invalid";
            }

            ViewBag.Message = message;

                purchaseViewModel.SupplierSelectListItems = _supplierManager.GetAll()
                                                                            .Select(c => new SelectListItem()
                                                                            {
                                                                                Value = c.Id.ToString(),
                                                                                Text = c.Name
                                                                            }).ToList();
                purchaseViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                                            .Select(c => new SelectListItem()
                                                                            {
                                                                                Value = c.Id.ToString(),
                                                                                Text = c.Name
                                                                            }).ToList();
                purchaseViewModel.ProductSelectListItems = _productManager.GetAll()
                                                                            .Select(c => new SelectListItem()
                                                                            {
                                                                                Value = c.Id.ToString(),
                                                                                Text = c.Name
                                                                            }).ToList();

                ViewBag.itemCategoryID = purchaseViewModel.CategorySelectListItems;
                ViewBag.itemProductID = purchaseViewModel.ProductSelectListItems;
                return View(purchaseViewModel);
            
        }


        [HttpGet]
        public ActionResult Edit(int Id)
        {
            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();

            for(int i=0; i<purchaseDetails.Count; i++)
            {
                if(purchaseDetails[i].Id==Id)
                {
                    purchaseViewModel= Mapper.Map<PurchaseViewModel>(purchaseDetails[i]);
                }
            }
            return View(purchaseViewModel);
        }

        [HttpGet]
        public ActionResult Detail()
        {

            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            purchaseViewModel.PurchaseDetails = _purchaseManager.GetAllDetails();
            
            return View(purchaseViewModel);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {

            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();

            purchaseViewModel.PurchaseDetail = _purchaseManager.GetPurchaseDetailsById(id);
            purchaseViewModel.ProductDetail = _productManager.GetById(purchaseViewModel.PurchaseDetail.ProductId);
            purchaseViewModel.CategoryDetail = _categoryManager.GetById(purchaseViewModel.PurchaseDetail.CategoryId);
            purchaseViewModel.Purchase = _purchaseManager.GetById(purchaseViewModel.PurchaseDetail.PurchaseId);
            purchaseViewModel.SupplierDetail = _supplierManager.GetById(purchaseViewModel.Purchase.SupplierId);



            ViewBag.itemCategoryID = purchaseViewModel.PurchaseDetail.CategoryId;
            return View(purchaseViewModel);
        }

        [HttpPost]
        public ActionResult Detail(PurchaseViewModel purchaseView)
        {
            DateTime oDate;
            List<Purchase> purchase = new List<Purchase>(); 
            PurchaseViewModel purchaseViewModel = new PurchaseViewModel();
            List<PurchaseDetails> purchaseDetailsList = new List<PurchaseDetails>();
            if (purchaseView.ItemSearch!="" && purchaseView.ItemSearch!=null)
            {
                Product productbyName = _productManager.GetByName(purchaseView.ItemSearch);
                Category category = _categoryManager.GetByName(purchaseView.ItemSearch);
                Supplier supplierByName=_supplierManager.GetByName(purchaseView.ItemSearch);
                Product productbyCode = _productManager.GetByCode (purchaseView.ItemSearch);
                Purchase purchaseByInvoice = _purchaseManager.GetByInvoiceNo(purchaseView.ItemSearch);
                
                

                if (supplierByName != null)
                {
                    purchase = _purchaseManager.GetBySupplierId(supplierByName.Id);
                }
                else if (purchaseByInvoice != null)
                {
                    purchase.Add(_purchaseManager.GetById(purchaseByInvoice.Id));
                }
                if (productbyName != null)
                {
                        purchaseDetailsList=_purchaseManager.GetPurchaseDetailsByProductId(productbyName.Id);
                }
                else if (category!=null)
                {
                        purchaseDetailsList=_purchaseManager.GetPurchaseDetailsByCategoryId(category.Id);
                }
                else if(productbyCode!=null)
                {
                    purchaseDetailsList.Add( _purchaseManager.GetPurchaseDetailsById(productbyCode.Id));
                }
                else 
                {
                    try
                    {
                        oDate = DateTime.Parse(purchaseView.ItemSearch);
                        Purchase purchaseByDate = _purchaseManager.GetPurchaseByDate(oDate);
                        if (purchaseByDate != null)
                        {
                            purchase.Add(_purchaseManager.GetById(purchaseByDate.Id));
                        }
                    }
                    catch
                    {}
                    
                }

                if (purchase.Count > 0)
                {
                    List<PurchaseDetails> pdl = new List<PurchaseDetails>();
                    for (int i = 0; i < purchase.Count; i++)
                    {
                        pdl = _purchaseManager.GetPurchaseDetailsBySupplierId(purchase[i].Id);
                        purchaseDetailsList.AddRange(pdl);
                    }
                }
                purchaseViewModel.PurchaseDetails = purchaseDetailsList;
                return View(purchaseViewModel);
            }

            purchaseViewModel.PurchaseDetails = _purchaseManager.GetAllDetails();
            return View(purchaseViewModel);
        }
        public ActionResult GetProductById(int Id)
        {
            var productList = _productManager.GetAll().Where(c => c.CategoryId == Id).ToList();

            var products = from s in productList select (new { s.Id, s.Name, s.Code});
            return Json(products, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetProductDetailsById(int productId,int categoryId)
        {
            var product = _productManager.GetById(productId);
            var purchaseDetails = _purchaseManager.GetPurchaseDetails(productId, categoryId);
            var purchaseQuantityDetails = _purchaseManager.GetPurchaseQuantityDetails(productId, categoryId);
            

            product.Recorder_Level= (from x in purchaseQuantityDetails select x.Quantity).Sum();
            

            if (purchaseDetails == null)
            {
                return Json(new { pc = product.Code, aq = product.Recorder_Level, pup = "0", pmrp = "0" });
            }
            else
            {
                return Json(new { pc = product.Code, aq = product.Recorder_Level, pup = purchaseDetails.UnitPrice, pmrp = purchaseDetails.MRP });
            }
        }

        [HttpPost]
        public ActionResult CheckInvoiceNo(string invo)
        {
            var purchase = _purchaseManager.GetByInvoiceNo(invo);
            if(purchase==null)
            {
                return Json(new { invo = ""});
            }
            else
            {
                return Json(new { invo = purchase.InvoiceNo });
            }
        }
    }
}