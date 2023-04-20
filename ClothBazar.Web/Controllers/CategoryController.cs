using ClothBazar.Entities;
using ClothBazar.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ClothBazar.Web.Controllers
{
    
    public class CategoryController : Controller
    {
    CategoriesServices categoryservice = new CategoriesServices();

        // GET: Category
        
        public ActionResult Index()
        {
                //var categories = categoryservice.GetCategories();
                List<Category> categories = categoryservice.GetCategories();
                return View(categories);
        }
        public ActionResult CategoryTable(string Search)
        {
            //var categories = categoryservice.GetCategories();
            List<Category> categories = categoryservice.GetCategories();
            if (string.IsNullOrEmpty(Search) == false)
            {
                categories = categories.Where(p => p.Name != null && p.Name.ToLower().Contains(Search.ToLower())).ToList();
            }
            return PartialView(categories);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Category category)
        {
            
         categoryservice.SaveCategory(category);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var category = categoryservice.GetCategorie(Id);
            return PartialView(category);
        }
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            categoryservice.UpdateCategory(category);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var category = categoryservice.GetCategorie(Id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            categoryservice.DeleteCategory(category.Id);
            return RedirectToAction("Index");
        }
    }
}