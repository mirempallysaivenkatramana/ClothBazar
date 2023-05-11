using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web;

namespace ClothBazar.Web.ViewModels
{
    public class ProductSearchViewModel
    {
        public int pageNo { get; set; }
        public List<Product> Products { get; set; }

        public string SearchTerm { get; set; }
    }
    public class NewProductViewModel
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId{ get; set; }
        public string ImageURL { get; set; }
        public List<Category> AvaliableCategories { get; set; }
    }
    public class EditProductViewModels
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string ImageURL { get; set; }
        public List<Category> AvaliableCategories { get; set; }

    }
    public class ProductViewModels
    {
        public Product Product { get; set; }
    }
}