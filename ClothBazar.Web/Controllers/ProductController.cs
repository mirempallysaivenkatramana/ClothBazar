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
       //ProductSearchViewModel model = new ProductSearchViewModel();
        // ProductsService productsService = new ProductsService();
        CategoriesServices categoryservice = new CategoriesServices();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ProductTable(string Search,int? pageNo)
        {
            ProductSearchViewModel model = new ProductSearchViewModel();

            //pageNo =pageNo.HasValue? pageNo:1;

            model.Products = ProductsService.Instance.GetProducts(/*pageNo.Value*/);
            if (string.IsNullOrEmpty(Search) == false)
            {
                model.SearchTerm = Search;
                model.Products = model.Products.Where(p => p.Name != null && p.Name.ToLower().Contains(Search.ToLower())).ToList();
            }
            /*foreach(var p in product)
            {
                if(p.Name==Search)
                {

                }
            }*/
            return PartialView(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            CategoriesServices cateroryservices = new CategoriesServices();
            NewProductViewModel model = new NewProductViewModel();

            model.AvaliableCategories = cateroryservices.GetCategories();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            CategoriesServices categoryservice = new CategoriesServices();
            var newproduct = new Product();
            newproduct.Name = model.Name;
            newproduct.Description = model.Description;
            newproduct.Price = model.Price;
            newproduct.Category = categoryservice.GetCategorie(model.CategoryId);
            ProductsService.Instance.SaveProduct(newproduct);
            return RedirectToAction("ProductTable");
        }
        [HttpGet]
        public ActionResult Edit(int Id)
        {
            EditProductViewModels model = new EditProductViewModels();
            var product =ProductsService.Instance.GetProduct(Id);

            model.Id = product.Id;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryId = product.Category!=null?product.Category.Id:0;
            model.AvaliableCategories = categoryservice.GetCategories();
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Edit(EditProductViewModels model)
        {
            var exitingproduct = ProductsService.Instance.GetProduct(model.Id);
            exitingproduct.Name = model.Name;
            exitingproduct.Description = model.Description;
            exitingproduct.Price = model.Price;
            exitingproduct.Category = categoryservice.GetCategorie(model.CategoryId);
            
            ProductsService.Instance.UpdateProduct(exitingproduct);
            return RedirectToAction("ProductTable");
        }
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            ProductsService.Instance.DeleteProduct(Id);
            return RedirectToAction("ProductTable");
        }
    }
}