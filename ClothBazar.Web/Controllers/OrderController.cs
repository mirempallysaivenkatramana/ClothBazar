using ClothBazar.Services;
using ClothBazar.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClothBazar.Web.Controllers
{
    public class OrderController : Controller
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
        // GET: Order
        public ActionResult Index(string userID, string status, int? pageno, int pageSize)
        {
            OrdersViewModels model = new OrdersViewModels();
            pageSize = 3;
            model.userID = userID;
            model.Status = status;
            model.Orders = OrdersService.Instance.SearchOrders(userID, status, pageno.Value, pageSize);
            
            pageno = pageno.HasValue ? pageno.Value > 0 ? pageno.Value : 1 : 1;
            var totalRecords = OrdersService.Instance.SearchOrdersCount(userID, status);
            model.pager = new Pager(totalRecords, pageno, 3);

            return View();
        }
        public ActionResult Details(int ID)
        {
            OrdersDetailsViewModels model = new OrdersDetailsViewModels();
            model.Order = OrdersService.Instance.GetOrderByID(ID);
            if (model.Order != null)
            {
                model.OrderBy = UserManager.FindById(model.Order.UserID);
            }
            model.AvaliableStatuses =new  List<string>(){"Pending","In Progress","Delivered" };

            return View(model);
        }
        public JsonResult ChangeStatus(string status,int ID)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = new { Success = OrdersService.Instance.UpdateOrderStatus(status,ID) };
            
            return result;
        }
    }
}