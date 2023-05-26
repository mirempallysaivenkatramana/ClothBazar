using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;


namespace ClothBazar.Web.Controllers
{
    [Authorize(Roles ="Admin")]
    public class CategoryController : Controller
    {
    //CategoriesServices categoryservice = new CategoriesServices();

        // GET: Category
        
        public ActionResult Index()
        {
                ////var categories = categoryservice.GetCategories();
                //List<Category> categories = categoryservice.GetCategories();
                return View();
        }
        public ActionResult CategoryTable(string Search,int? pageno)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();
            model.SearchTerm = Search;
            pageno = pageno.HasValue ? pageno.Value > 0 ? pageno.Value : 1 : 1;
            var totalRecords = CategoriesServices.Instance.GetCategoriesCount(Search);
            //var categories = categoryservice.GetCategories();
            model.Categories = CategoriesServices.Instance.GetCategories(Search,pageno.Value);
            if (model.Categories != null)
            {
                
                model.pager = new Pager(totalRecords, pageno,3);
               // model.Categories=model.Categories.OrderBy(x => x.Id).Skip((pageNo - 1) * pagesize).Take(pagesize).ToList();
                
                return PartialView("categoryTable", model);
            }
            else
            {
                return HttpNotFound();
            }
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
            if (ModelState.IsValid)
            {
                var newcategory = new Category();
                newcategory.Name = model.Name;
                newcategory.Description = model.Description;
                newcategory.ImageURL = model.ImageURL;

                CategoriesServices.Instance.SaveCategory(newcategory);
                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }

        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();
            var category = CategoriesServices.Instance.GetCategorie(Id);

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
            var exitingcategory = CategoriesServices.Instance.GetCategorie(model.Id);
            exitingcategory.Name = model.Name;
            exitingcategory.Description = model.Description;
            exitingcategory.ImageURL = model.ImageURL;
            exitingcategory.IsFeatured = model.IsFeatured;

            CategoriesServices.Instance.UpdateCategory(exitingcategory);
            return RedirectToAction("CategoryTable");
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var category = CategoriesServices.Instance.GetCategorie(Id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Delete(Category category)
        {
            CategoriesServices.Instance.DeleteCategory(category.Id);
            return RedirectToAction("Index");
        }
    }
}