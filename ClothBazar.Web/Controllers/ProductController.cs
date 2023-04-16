using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductsService productsService = new ProductsService();
       // CategoriesServices categoryservice = new CategoriesServices();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ProductTable(string Search)
        {
            var product = productsService.GetProducts();
            if (string.IsNullOrEmpty(Search) == false)
            {
                product = product.Where(p => p.Name != null && p.Name.ToLower().Contains(Search.ToLower())).ToList();
            }
            /*foreach(var p in product)
            {
                if(p.Name==Search)
                {

                }
            }*/
            return PartialView(product);
        }
        [HttpGet]
        public ActionResult Create()
        {
            CategoriesServices cateroryservices = new CategoriesServices();
            var categories = cateroryservices.GetCategories();
            return PartialView(categories);
        }
        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            CategoriesServices categoryservice = new CategoriesServices();
            var newproduct = new Product();
            newproduct.Name = model.Name;
            newproduct.Description = model.Description;
            newproduct.Price = model.Price;
            newproduct.Category = categoryservice.GetCategorie(model.CategoryId);
            productsService.SaveProduct(newproduct);
            return RedirectToAction("ProductTable");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var product = productsService.GetProduct(Id);
            return PartialView(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            productsService.UpdateProduct(product);
            return RedirectToAction("ProductTable");
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            productsService.DeleteProduct(Id);
            return RedirectToAction("ProductTable");
        }
    }
}