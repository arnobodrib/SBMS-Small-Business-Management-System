using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebProject.Model.Model;
using WebProject.Models;
using WebProject.BLL.BLL;
using AutoMapper;

namespace WebProject.Controllers
{
    public class SupplierController:Controller
    {
        SupplierManager _supplierManager = new SupplierManager();

        //[HttpGet]
        //public ActionResult Add()
        //{
        //    SupplierViewModel supplierViewModel = new SupplierViewModel();

        //    supplierViewModel.Suppliers = _supplierManager.GetAll();
        //    return View(supplierViewModel);
        //}


        public ActionResult Add()
        {
            SupplierViewModel supplierViewModel = new SupplierViewModel();
            supplierViewModel.Suppliers = _supplierManager.GetAll();
            return View(supplierViewModel);
        }


        [HttpPost]
        public ActionResult Add(SupplierViewModel supplierViewModel)
        {

            string message = "";

            if (ModelState.IsValid)
            {
                Supplier supplier = Mapper.Map<Supplier>(supplierViewModel);
                string errMsg = _supplierManager.UniqueTest(supplier);

                if (errMsg == "")
                {
                    if (_supplierManager.Add(supplier))
                    {
                        message = "Saved";
                    }
                    else
                    {
                        message = "Not saved";
                    }
                }
                else
                {
                    message = errMsg;
                }
            }
            else
            {
                message = "Modelstate is invalid";
            }

            ViewBag.Message = message;
            supplierViewModel.Suppliers = _supplierManager.GetAll();

            return View(supplierViewModel);
        }

        [HttpGet]
        public ActionResult Search()
        {
            SupplierViewModel supplierViewModel = new SupplierViewModel();
            supplierViewModel.Suppliers = _supplierManager.GetAll();
            return View(supplierViewModel);
        }

        [HttpPost]
        public ActionResult Search(SupplierViewModel supplierViewModel)
        {
            var suppliers = _supplierManager.GetAll();
            if (supplierViewModel.Name != null)
            {
                suppliers = suppliers.Where(s => s.Name.ToUpper().Contains(supplierViewModel.Name.ToUpper())).ToList();
            }
            if (supplierViewModel.Contact != null)
            {
                suppliers = suppliers.Where(s => s.Contact.Contains(supplierViewModel.Contact)).ToList();
            }
            if (supplierViewModel.Email != null)
            {
                suppliers = suppliers.Where(s => s.Email.ToUpper().Contains(supplierViewModel.Email.ToUpper())).ToList();
            }




            supplierViewModel.Suppliers = suppliers;
            return View(supplierViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            SupplierViewModel supplierViewModel = new SupplierViewModel();
            var supplier = _supplierManager.GetById(id);
            supplierViewModel = Mapper.Map<SupplierViewModel>(supplier);
            supplierViewModel.Suppliers = _supplierManager.GetAll();
            return View(supplierViewModel);
        }

        [HttpPost]
        public ActionResult Edit(SupplierViewModel supplierViewModel)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                Supplier supplier = Mapper.Map<Supplier>(supplierViewModel);
                if (_supplierManager.Update(supplier))
                {
                    message = "Updated";
                }
                else
                {
                    message = "Information is Not Updated";
                }
            }
            else
            {
                message = "Invalid";
            }
            ViewBag.Message = message;
            supplierViewModel.Suppliers = _supplierManager.GetAll();

            return View(supplierViewModel);
        }


        public JsonResult IsCodeExist(string code)
        {
            bool isExists = false;
            var nameList = _supplierManager.GetAll().Where(c => c.Code == code);


            if (nameList.Count() > 0)
            {
                isExists = true;
            }

            return Json(isExists, JsonRequestBehavior.AllowGet);
        }


        public JsonResult IsNameExist(string name)
        {
            bool isExists = false;
            var nameList = _supplierManager.GetAll().Where(c => c.Name == name);


            if (nameList.Count() > 0)
            {
                isExists = true;
            }

            return Json(isExists, JsonRequestBehavior.AllowGet);
        }


        public JsonResult IsContactExist(string contact)
        {
            bool isExists = false;
            var nameList = _supplierManager.GetAll().Where(c => c.Contact == contact);


            if (nameList.Count() > 0)
            {
                isExists = true;
            }

            return Json(isExists, JsonRequestBehavior.AllowGet);
        }



    }
}