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
        ProductsService productsservices = new ProductsService();
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

                model.CartProducts = productsservices.GetProducts(model.CartProductIds);
            }
            return View(model);
        }
    }
}