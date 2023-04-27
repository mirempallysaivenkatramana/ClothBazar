using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
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
                ////var categories = categoryservice.GetCategories();
                //List<Category> categories = categoryservice.GetCategories();
                return View();
        }
        public ActionResult CategoryTable(string Search)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();
            //var categories = categoryservice.GetCategories();
            model.Categories = categoryservice.GetCategories();
            if (string.IsNullOrEmpty(Search) == false)
            {
                model.SearchTerm = Search;
                model.Categories = model.Categories.Where(p => p.Name != null && p.Name.ToLower().Contains(Search.ToLower())).ToList();
            }
            return PartialView("categoryTable", model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();

            //model.AvaliableCategories = categoryservice.GetCategories();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            var newcategory = new Category();
            newcategory.Name = model.Name;
            newcategory.Description = model.Description;
            newcategory.ImageURL = model.ImageURL;

            categoryservice.SaveCategory(newcategory);
            return RedirectToAction("CategoryTable");

        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();
            var category = categoryservice.GetCategorie(Id);

            model.Id = category.Id;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.IsFeatured = category.IsFeatured;
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
           
            //return RedirectToAction("Index");
            var exitingcategory = categoryservice.GetCategorie(model.Id);
            exitingcategory.Name = model.Name;
            exitingcategory.Description = model.Description;
            exitingcategory.ImageURL = model.ImageURL;
            exitingcategory.IsFeatured = model.IsFeatured;

            categoryservice.UpdateCategory(exitingcategory);
            return RedirectToAction("CategoryTable");
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