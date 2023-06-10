using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ClothBazar.Services
{
    public class OrdersService
    {
        CBContext context = new CBContext();
        #region Singleton
        public static OrdersService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrdersService();
                }
                return instance;
            }
        }
        private static OrdersService instance { get; set; } //private modefier because instance should not be created out side class
        private OrdersService()
        {
        }
        #endregion

        public List<Order> SearchOrders(string userID, string status, int pageNo, int pageSize)
        {
            var Orders = context.Orders.ToList();
            
            if (!string.IsNullOrEmpty(userID))
            {
                Orders = Orders.Where(x => x.UserID.ToLower().Contains(userID.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(status))
            {
                Orders = Orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
            }
            return Orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public int SearchOrdersCount(string userID, string status)
        {
            var Orders = context.Orders.ToList();

            if (!string.IsNullOrEmpty(userID))
            {
                Orders = Orders.Where(x => x.UserID.ToLower().Contains(userID.ToLower())).ToList();
            }
            if (!string.IsNullOrEmpty(status))
            {
                Orders = Orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
            }
            return Orders.Count;
        }

        public Order GetOrderByID(int ID)
        {
            
            return context.Orders.Where(x=>x.Id==ID).Include(x=>x.OrderItems).Include("OrderItems.Product").FirstOrDefault();
        }

        public bool UpdateOrderStatus(string status,int ID)
        {
            var order = context.Orders.Find(ID);
            order.Status = status;
            context.Entry(order).State = EntityState.Modified;
            return context.SaveChanges()>0;
            
        }
    }
}
