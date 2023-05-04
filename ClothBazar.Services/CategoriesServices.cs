using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothBazar.Database;
using System.Data.Entity;

namespace ClothBazar.Services
{
    public class CategoriesServices
    {
        #region Singleton
        public static CategoriesServices Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CategoriesServices();
                }
                return instance;
            }
        }
        private static CategoriesServices instance { get; set; } //private modefier because instance should not be created out side class
        private CategoriesServices()
        {
        }
        #endregion
        CBContext context = new CBContext();
        public List<Category> GetCategories(string Search,int pageno)
        {
            int pagesize = 3;
            if (string.IsNullOrEmpty(Search) == false)
            {
                return context.Categories.Where(Category => Category.Name != null &&
                Category.Name.ToLower().Contains(Search.ToLower()))
                .OrderBy(x => x.Id)
                .Skip((pageno - 1) * pagesize)
                .Take(pagesize)
                .Include(x => x.Products)
                .ToList();
            }
            else
            {
                return context.Categories
                    .OrderBy(x => x.Id)
                    .Skip((pageno - 1) * pagesize)
                    .Take(pagesize)
                    .Include(x => x.Products)
                    .ToList();
            }
            }
        public int GetCategoriesCount(string Search)
        {
            if (string.IsNullOrEmpty(Search) == false)
            {
                return context.Categories.Where(Category => Category.Name != null &&
                Category.Name.ToLower().Contains(Search.ToLower())).Count();
            }
            else
            {
                return context.Categories.Count();
            }

            
        }
        public Category GetCategorie(int Id)
        {
            return context.Categories.Find(Id);
        }
        public List<Category> GetallCategories()
        {
            //int pagesize = 5;
            return context.Categories.ToList();
        }
        public List<Category> GetFeaturedCategories()
        {
            return context.Categories.Where(x=>x.IsFeatured && x.ImageURL != null).ToList();
        }
        public void SaveCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }
        public void UpdateCategory(Category category)
        {
            context.Entry(category).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void DeleteCategory(int Id)
        {
            var category = context.Categories.Find(Id);
            context.Categories.Remove(category);
            context.SaveChanges();
        }
    }
}
