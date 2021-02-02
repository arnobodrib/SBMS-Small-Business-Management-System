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
    public class CustomerController : Controller
    {
        CustomerManager _customerManager = new CustomerManager();

        [HttpGet]
        public ActionResult Add()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customerViewModel.Customers = _customerManager.GetAll();
            return View(customerViewModel);
        }

        [HttpPost]
        public ActionResult Add(CustomerViewModel customerViewModel)
        {

            string message = "";

            if (ModelState.IsValid)
            {
                Customer customer = Mapper.Map<Customer>(customerViewModel);

                if (_customerManager.Add(customer))
                {
                    message = "Saved";
                }
                else
                {
                    message = "not saved";
                }
            }
            else
            {
                message = "modelstate is invalid";
            }

            ViewBag.Message = message;

            ModelState.Clear();
            CustomerViewModel NcustomerViewModel = new CustomerViewModel();

            NcustomerViewModel.Customers = _customerManager.GetAll();

            return View(NcustomerViewModel);
        }

        [HttpGet]
        public ActionResult Search()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customerViewModel.Customers =  _customerManager.GetAll();
            return View(customerViewModel);
        }

        [HttpGet]
        public ActionResult CustomerSearch()
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            customerViewModel.Customers = _customerManager.GetAll();
            return View(customerViewModel);
        }

        [HttpPost]
        public ActionResult Search(CustomerViewModel customerViewModel)
        {
            var customers =  _customerManager.GetAll();
            if (customerViewModel.Name != null)
            {
                customers = customers.Where(c => c.Name.ToUpper().Contains(customerViewModel.Name.ToUpper())).ToList();
            }
            if (customerViewModel.Email != null)
            {
                customers = customers.Where(c => c.Email.Contains(customerViewModel.Email)).ToList();
            }
            if (customerViewModel.Contact != null)
            {
                customers = customers.Where(c => c.Contact.Contains(customerViewModel.Contact)).ToList();
            }
            customerViewModel.Customers = customers;
            return View(customerViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            var customer = _customerManager.GetById(id);
            customerViewModel = Mapper.Map<CustomerViewModel>(customer);
            customerViewModel.Customers = _customerManager.GetAll();
            return View(customerViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CustomerViewModel customerViewModel)
        {
            string message = "";

            if (ModelState.IsValid)
            {
                Customer customer = Mapper.Map<Customer>(customerViewModel);

                if (_customerManager.Update(customer))
                {
                    message = "Updated";
                }
                else
                {
                    message = "not Updated";
                }
            }
            else
            {
                message = "modelstate is invalid";
            }

            ViewBag.Message = message;

            ModelState.Clear();
            CustomerViewModel NcustomerViewModel = new CustomerViewModel();

            NcustomerViewModel.Customers = _customerManager.GetAll();

            return View(NcustomerViewModel);
        }

        //Unique code Test
        public JsonResult IsCodeExist(string code)
        {
            bool isExists = false;
            var codeList = _customerManager.GetAll().Where(c => c.Code == code);


            if (codeList.Count() > 0)
            {
                isExists = true;
            }

            return Json(isExists, JsonRequestBehavior.AllowGet);
        }

        //Unique Email Test
        public JsonResult IsEmailExist(string email)
        {
            bool isExists = false;
            var emailList = _customerManager.GetAll().Where(c => c.Email == email);


            if (emailList.Count() > 0)
            {
                isExists = true;
            }

            return Json(isExists, JsonRequestBehavior.AllowGet);
        }

        //Unique Contact Test
        public JsonResult IsContactExist(string contact)
        {
            bool isExists = false;
            var contactList = _customerManager.GetAll().Where(c => c.Contact == contact);


            if (contactList.Count() > 0)
            {
                isExists = true;
            }

            return Json(isExists, JsonRequestBehavior.AllowGet);
        }

        //Unique code Test for update
        public JsonResult IsCodeExistForUpdate(int customerId,string code)
        {
            bool isExists = false;
            var codeList = _customerManager.GetAll().Where(c => c.Code == code && c.Id!= customerId);


            if (codeList.Count() > 0)
            {
                isExists = true;
            }

            return Json(isExists, JsonRequestBehavior.AllowGet);
        }

        //Unique email Test for update
        public JsonResult IsEmailExistForUpdate(int customerId, string email)
        {
            bool isExists = false;
            var emailList = _customerManager.GetAll().Where(c => c.Email == email && c.Id != customerId);


            if (emailList.Count() > 0)
            {
                isExists = true;
            }

            return Json(isExists, JsonRequestBehavior.AllowGet);
        }

        //Unique contact Test for update
        public JsonResult IsContactExistForUpdate(int customerId, string contact)
        {
            bool isExists = false;
            var contactList = _customerManager.GetAll().Where(c => c.Contact == contact && c.Id != customerId);


            if (contactList.Count() > 0)
            {
                isExists = true;
            }

            return Json(isExists, JsonRequestBehavior.AllowGet);
        }


    }
}