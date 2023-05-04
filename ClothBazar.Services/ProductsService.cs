using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothBazar.Database;
using System.Data.Entity;
using System.Security.Cryptography;

namespace ClothBazar.Services
{
    public class ProductsService
    {
        #region Singleton
        public static ProductsService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProductsService();
                }
                return instance;
            }
        }
        private static ProductsService instance { get; set; } //private modefier because instance should not be created out side class
        private ProductsService()
        {
        }
        #endregion
        CBContext context = new CBContext();
        public List<Product> GetProducts(int pageNo)
        {
            int pagesize = 5;//int.Parse(ConfigurationsService.Instance.GetConfig("listingpageSize").Value);
            return context.Products.OrderBy(x=>x.Id).Skip((pageNo-1)*pagesize).Take(pagesize).Include(x=>x.Category).ToList();//to skip
        }
        public List<Product> GetProducts(List<int> Ids)
        {
            return context.Products.Where(product => Ids.Contains(product.Id)).ToList();
        }
        public Product GetProduct(int Id)
        {
            return context.Products.Where(x => x.Id == Id).Include(x => x.Category).FirstOrDefault();
        }
        public void SaveProduct(Product product)
        {
            context.Entry(product).State = System.Data.Entity.EntityState.Unchanged;

            context.Products.Add(product);
            context.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            // context.Entry(product).State = EntityState.Detached;
            context.Entry(product).State = EntityState.Modified;
            //context.Entry(product).State = EntityState.Unchanged;

            //context.SaveChanges();
            // context.Entry(product).CurrentValues.SetValues(product);
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
