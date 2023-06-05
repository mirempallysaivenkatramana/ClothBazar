using ClothBazar.Entities;
using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class ShopController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

       

        

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
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
        [Authorize]
        public ActionResult Checkout()
        {
            CheckoutViewModels model = new CheckoutViewModels();

           var CartProductsCookie = Request.Cookies["CartProducts"];
            if(CartProductsCookie!=null&& !string.IsNullOrEmpty(CartProductsCookie.Value))
            {
                //var productIds = CartProductsCookie.Value;
                //var ids = productIds.Split('-');
                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();


                model.CartProductIds = CartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                model.CartProducts = ProductsService.Instance.GetProducts(model.CartProductIds);
                model.User = UserManager.FindById(User.Identity.GetUserId());
            }
            return View(model);
        }
        public ActionResult PlaceOrder(string productIDs)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            if (!string.IsNullOrEmpty(productIDs))
            {

                var productQuantities = productIDs.Split('-').Select(x => int.Parse(x)).Distinct().ToList();
                //var pIds = productIDs.Split( new char[]{'-'}).Select(x => int.Parse(x)).Distinct().ToList();
                var boughtProducts = ProductsService.Instance.GetProducts(productQuantities.Distinct().ToList());

                Order neworder = new Order();
                neworder.UserID = User.Identity.GetUserId();
                neworder.OrderedAt = DateTime.Now;
                neworder.Status = "pending";
                neworder.TotalAmount = boughtProducts.Sum(x => x.Price * productQuantities.Where(productID => productID == x.Id).Count());


                neworder.OrderItems = new List<OrderItem>();
                neworder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem() { ProductID = x.Id, Quantity = productQuantities.Where(productID => productID == x.Id).Count() }));
                var rowseffected = ShopService.Instance.SaveOrder(neworder);


                result.Data = new { success = true, rows = rowseffected };
            }
            else
            {
                result.Data = new { success = false };
            }
            return result;
        }
    }
}