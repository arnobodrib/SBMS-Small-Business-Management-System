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
    public class CategoryController : Controller
    {
        CategoryManager _categoryManager = new CategoryManager();

        [HttpGet]
        public ActionResult Add()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.Categories = _categoryManager.GetAll();
            return View(categoryViewModel);
        }

        [HttpPost]
        public ActionResult Add(CategoryViewModel categoryViewModel)
        {

            string message = "";

            if (ModelState.IsValid)
            {
                Category category = Mapper.Map<Category>(categoryViewModel);
                string errMsg = _categoryManager.UniqueTest(category);

                if (errMsg == "")
                {
                    if (_categoryManager.Add(category))
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
                    message = errMsg;
                }
            }
            else
            {
                message = "modelstate is invalid";
            }

            ViewBag.Message = message;
            categoryViewModel.Categories = _categoryManager.GetAll();

            return View(categoryViewModel);
        }

        [HttpGet]
        public ActionResult Search()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.Categories = _categoryManager.GetAll();
            return View(categoryViewModel);
        }

        [HttpPost]
        public ActionResult Search(CategoryViewModel categoryViewModel)
        {
            var categories = _categoryManager.GetAll();
            if (categoryViewModel.Code != null)
            {
                categories = categories.Where(c => c.Code.Contains(categoryViewModel.Code)).ToList();
            }

            if (categoryViewModel.Name != null)
            {
                categories = categories.Where(c => c.Name.ToUpper().Contains(categoryViewModel.Name.ToUpper()))
                    .ToList();
            }

            categoryViewModel.Categories = categories;
            return View(categoryViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            var category = _categoryManager.GetById(id);
            categoryViewModel = Mapper.Map<CategoryViewModel>(category);
            categoryViewModel.Categories = _categoryManager.GetAll();
            return View(categoryViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                Category category = Mapper.Map<Category>(categoryViewModel);
                if (_categoryManager.Update(category))
                {
                    message += "Updated";
                }
                else
                {
                    message += "Not Updated";
                }
            }
            else
            {
                message = "modelstate is invalid";
            }

            ViewBag.Message = message;
            categoryViewModel.Categories = _categoryManager.GetAll();

            return View(categoryViewModel);
        }

        public JsonResult IsNameExist(string name)
        {
            bool isExists = false;
            var nameList = _categoryManager.GetAll().Where(c => c.Name == name);


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
            var nameList = _categoryManager.GetAll().Where(c => c.Code == code);


            if (nameList.Count() > 0)
            {
                isExists = true;
            }

            return Json(isExists, JsonRequestBehavior.AllowGet);


        }
    }
}
