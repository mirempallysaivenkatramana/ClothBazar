using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ClothBazar.Services;

namespace ClothBazar.Web.Controllers
{
    public class ProductController : Controller
    {
        ProductsService productsService = new ProductsService();
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
            return PartialView();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            productsService.SaveProduct(product);
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