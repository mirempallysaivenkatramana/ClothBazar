using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        // ProductsService productsservices = new ProductsService();
        
       public ActionResult Index(string searchTerm,int? minimumPrice,int? maximumPrice,int? categoryID,int? sortBy,int? pageNo,int pageSize=10)
        {
            ShopViewModel model = new ShopViewModel();

            model.FeaturedCategories = CategoriesServices.Instance.GetFeaturedCategories();
            model.MaximumPrice = ProductsService.Instance.GetMaximumPrice();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value,10);
            model.SortBy = sortBy;
            model.CategoryID = categoryID;
            int totalCount = 300;// ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);

            model.Pager = new Pager(totalCount,pageNo);
            return View(model);
        }
        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy,int? pageNo, int pageSize)
        {
            FilterProductsViewModel model = new FilterProductsViewModel();

            model.Products = ProductsService.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, 10);
            int totalCount = ProductsService.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);

            model.Pager = new Pager(totalCount, pageNo);
            return View(model);
        }
        public ActionResult Checkout()
        {
            CheckoutViewModels model = new CheckoutViewModels();

           var CartProductsCookie = Request.Cookies["CartProducts"];
            if(CartProductsCookie!=null)
            {
                //var productIds = CartProductsCookie.Value;
                //var ids = productIds.Split('-');
                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();


                model.CartProductIds = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = ProductsService.Instance.GetProducts(model.CartProductIds);
            }
            return View(model);
        }
    }
}