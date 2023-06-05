using ClothBazar.Entities;
using ClothBazar.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModels
{
    public class OrdersViewModels
    {
        public List<Order> Orders { get; set; }
        public string userID { get;  set; }
        public Pager pager { get;  set; }
        public string Status { get;  set; }
    }
    public class OrdersDetailsViewModels
    {
        public Order Order { get; set; }
        public ApplicationUser OrderBy { get; set; }
        public List<string> AvaliableStatuses { get;  set; }
    }
}