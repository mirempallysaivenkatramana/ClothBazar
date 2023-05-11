using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool IsLatestProducts,int? CategoryId/*,bool? IsCategoryProducts*/)
        {
            ProductsWidgetViewModel model = new ProductsWidgetViewModel();
            model.IsLatestProducts = IsLatestProducts;
            if (IsLatestProducts)
            {
                model.Products = ProductsService.Instance.GetLatestProducts(4);
            }
            else if(CategoryId.HasValue&&CategoryId>0)
            {
                model.Products = ProductsService.Instance.GetProductsByCategory(CategoryId.Value,4);
            }
            else
            {
                model.Products = ProductsService.Instance.GetProducts(1, 8);
            }
            return PartialView(model);
        }
    }
}