using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using WebProject.Model.Model;
using WebProject.BLL.BLL;

using WebProject.Models;

namespace WebApp.Controllers
{
    public class ProductController : Controller
    {
        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();

        [HttpGet]
        public ActionResult Add()
        {
            ProductViewModel productViewModel = new ProductViewModel();
            productViewModel.products = _productManager.GetAll();
            productViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                                        .Select(c => new SelectListItem()
                                                                        {
                                                                            Value = c.Id.ToString(),
                                                                            Text = c.Name
                                                                        }).ToList();
            return View(productViewModel);
        }
        [HttpPost]
        public ActionResult Add(ProductViewModel productViewModel)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                Product product = new Product();

                product.Code = productViewModel.Code;
                product.CategoryId = productViewModel.CategoryId;
                product.Code = productViewModel.Code;
                product.Name = productViewModel.Name;
                product.Recorder_Level = productViewModel.Recorder_Level;
                product.Description = productViewModel.Description;

                if (_productManager.Add(product))
                {
                    message += "Saved";
                }
                else
                {
                    message += "Not Saved";
                }
            }

            ViewBag.Message = message;
            productViewModel = new ProductViewModel();
            productViewModel.products = _productManager.GetAll();
            productViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                                        .Select(c => new SelectListItem()
                                                                        {
                                                                            Value = c.Id.ToString(),
                                                                            Text = c.Name
                                                                        }).ToList();
            return View(productViewModel);
        }


        [HttpGet]
        public ActionResult Search()
        {
            ProductViewModel productViewModel = new ProductViewModel();

            productViewModel.products = _productManager.GetAll();
            productViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                                        .Select(c => new SelectListItem()
                                                                        {
                                                                            Value = c.Id.ToString(),
                                                                            Text = c.Name
                                                                        }).ToList();
            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult Search(ProductViewModel productViewModel)
        {

            var products = _productManager.GetAll();

            if (productViewModel.Code != null)
            {
                products = products.Where(c => c.CategoryCode.Contains(productViewModel.Code)).ToList();
            }
            if (productViewModel.Name != null)
            {
                products = products.Where(c => c.Name.ToUpper().Contains(productViewModel.Name.ToUpper())).ToList();
            }

            productViewModel.products = products;
            productViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                                        .Select(c => new SelectListItem()
                                                                        {
                                                                            Value = c.Id.ToString(),
                                                                            Text = c.Name
                                                                        }).ToList();

            //productViewModel.CategorySelectListItems = _departmentManager
            //    .GetAll()
            //    .Select(c => new SelectListItem()
            //    {
            //        Value = c.Id.ToString(),
            //        Text = c.Name
            //    }).ToList();

            return View(productViewModel);
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            string message = "";
            Product product = _productManager.GetById(id);
            ProductViewModel productViewModel = new ProductViewModel();

            productViewModel.Code = product.Code;
            productViewModel.Name = product.Name;
            //productViewModel.UnitPrice = product.UnitPrice;
            productViewModel.Recorder_Level = product.Recorder_Level;
            productViewModel.Description = product.Description;

            productViewModel.products = _productManager.GetAll();

            productViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                                        .Select(c => new SelectListItem()
                                                                        {
                                                                            Value = c.Id.ToString(),
                                                                            Text = c.Name
                                                                        }).ToList();
            return View(productViewModel);
        }

        [HttpPost]
        /*
        public ActionResult Edit(ProductViewModel productViewModel)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                Product product = new Product();
                product.Id = productViewModel.Id;

                product.CategoryCode = productViewModel.CategoryCode;
                product.Code = productViewModel.Code;
                product.Name = productViewModel.Name;
                //product.UnitPrice = productViewModel.UnitPrice;
                product.Recorder_Level = productViewModel.Recorder_Level;
                product.Description = productViewModel.Description;

                if (_productManager.Update(product))
                {
                    message += "Saved";
                }
                else
                {
                    message += "Not Saved";
                }
            }
            else
            {
                message = "ModelState is invalied!";
            }

            ViewBag.Message = message;
            productViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                                        .Select(c => new SelectListItem()
                                                                        {
                                                                            Value = c.Id.ToString(),
                                                                            Text = c.Name
                                                                        }).ToList();
            productViewModel = new ProductViewModel();
            productViewModel.products = _productManager.GetAll();
            return View(productViewModel);
        }*/
        public ActionResult Edit(ProductViewModel productViewModel)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                Product product = new Product();

                product.Id = productViewModel.Id;
                product.Code = productViewModel.Code;
                product.CategoryId = productViewModel.CategoryId;
                product.Name = productViewModel.Name;
                product.Recorder_Level = productViewModel.Recorder_Level;
                product.Description = productViewModel.Description;

                if (_productManager.Update(product))
                {
                    message += "Updated";
                }
                else
                {
                    message += "Not Saved";
                }
            }

            ViewBag.Message = message;
            productViewModel = new ProductViewModel();
            productViewModel.products = _productManager.GetAll();
            productViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                                        .Select(c => new SelectListItem()
                                                                        {
                                                                            Value = c.Id.ToString(),
                                                                            Text = c.Name
                                                                        }).ToList();
            return View(productViewModel);
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            string message = "";
            Product product = _productManager.GetById(id);
            ProductViewModel productViewModel = new ProductViewModel();

            productViewModel.Code = product.Code;
            productViewModel.Name = product.Name;
            //productViewModel.UnitPrice = product.UnitPrice;
            productViewModel.Recorder_Level = product.Recorder_Level;
            productViewModel.Description = product.Description;

            productViewModel.products = _productManager.GetAll();
            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult Delete(ProductViewModel productViewModel)
        {
            string message = "";
           
                Product product = new Product();
                product.Code = productViewModel.Code;
                product.Name = productViewModel.Name;
                //product.UnitPrice = productViewModel.UnitPrice;
                product.Recorder_Level = productViewModel.Recorder_Level;
                product.Description = productViewModel.Description;

                if (_productManager.Delete(product))
                {
                    message += "Deleted Successfully";
                }
                else
                {
                    message += "Not Deleted";
                }

            ViewBag.Message = message;

            productViewModel = new ProductViewModel();
            productViewModel.products = _productManager.GetAll();
            return View(productViewModel);
        }

        public JsonResult IsNameExist(string name)
        {
            bool isExists = false;
            var nameList = _productManager.GetAll().Where(c => c.Name == name);


            if (nameList.Count() > 0)
            {
                isExists = true;
            }

            return Json(isExists, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult IsCodeExist(string code)
        {
            bool isExists = false;
            var nameList = _productManager.GetAll().Where(c => c.Code == code);


            if (nameList.Count() > 0)
            {
                isExists = true;
            }

            return Json(isExists, JsonRequestBehavior.AllowGet);
        }
    }
}
    
