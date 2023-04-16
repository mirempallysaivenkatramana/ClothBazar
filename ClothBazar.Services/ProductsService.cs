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
    public class ProductsService
    {
        CBContext context = new CBContext();
        public List<Product> GetProducts()
        {
            return context.Products.Include(x=>x.Category).ToList();
        }
        public Product GetProduct(int Id)
        {
            return context.Products.Find(Id);
        }
        public void SaveProduct(Product product)
        {
            context.Entry(product).State = System.Data.Entity.EntityState.Unchanged;

            context.Products.Add(product);
            context.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            context.Entry(product).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }
        public void DeleteProduct(int Id)               
        {
            var product = context.Products.Find(Id);
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }
}
