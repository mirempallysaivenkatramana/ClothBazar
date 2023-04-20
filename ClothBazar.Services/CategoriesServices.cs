using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothBazar.Database;

namespace ClothBazar.Services
{
    public class CategoriesServices
    {
        CBContext context = new CBContext();
        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }
        public Category GetCategorie(int Id)
        {
            return context.Categories.Find(Id);
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
