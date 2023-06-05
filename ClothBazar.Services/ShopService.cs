using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class ShopService
    {
        CBContext context = new CBContext();
        #region Singleton
        public static ShopService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShopService();
                }
                return instance;
            }
        }
        private static ShopService instance { get; set; } //private modefier because instance should not be created out side class
        private ShopService()
        {
        }
        #endregion

        public int SaveOrder(Order order)
        {
            context.Orders.Add(order);
            return context.SaveChanges();
        }
    }
}
