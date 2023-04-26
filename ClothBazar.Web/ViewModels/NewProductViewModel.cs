using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClothBazar.Web.ViewModels
{
    //public class NewProductViewModel
    //{
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public decimal Price { get; set; }
    //    public int CategoryId { get; set; }
    //}
    public class CategorySearchViewModel
    {
        public List<Category> Categories { get; set; }

        public string SearchTerm { get; set; }
    }
    public class NewCategoryViewModel
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
    }
}