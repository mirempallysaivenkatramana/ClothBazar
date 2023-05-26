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

        public int GetMaximumPrice()
        {
            return (int)(context.Products.Max(p => p.Price));
        }
        public List<Product> GetProducts(int pageNo)
        {
            int pagesize = 5;//int.Parse(ConfigurationsService.Instance.GetConfig("listingpageSize").Value);
            return context.Products.OrderBy(x=>x.Id).Skip((pageNo-1)*pagesize).Take(pagesize).Include(x=>x.Category).ToList();//to skip
        }
         
        public List<Product> GetProducts(int pageNo,int pagesize)
        {
            //int pagesize = 5;//int.Parse(ConfigurationsService.Instance.GetConfig("listingpageSize").Value);
            return context.Products.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pagesize).Take(pagesize).Include(x => x.Category).ToList();//to skip
        }
        public List<Product> GetProductsByCategory(int CategoryId, int pagesize)
        {
            //int pagesize = 5;//int.Parse(ConfigurationsService.Instance.GetConfig("listingpageSize").Value);
            return context.Products.Where(x=>x.Category.Id==CategoryId).OrderByDescending(x => x.Id).Take(pagesize).Include(x => x.Category).ToList();//to skip
        }
        public List<Product> GetLatestProducts(int numberofproducts)
        {
           // int pagesize = 5;//int.Parse(ConfigurationsService.Instance.GetConfig("listingpageSize").Value);
            return context.Products.OrderByDescending(x => x.Id).Take(numberofproducts).Include(x => x.Category).ToList();//to skip
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
            context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;

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

        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy,int pageNo,int pageSize)
        {
            var products = context.Products.ToList();
            if (categoryID.HasValue)
            {
                products = products.Where(x => x.Category.Id == categoryID.Value).ToList();
            }
            if(!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            }
            else
            {
                products = context.Products.ToList();
            }
            if (minimumPrice.HasValue)
            {
                products = products.Where(x => x.Price == minimumPrice.Value).ToList();
            }
            if (maximumPrice.HasValue)
            {
                products = products.Where(x => x.Price == maximumPrice.Value).ToList();
            }
            if(sortBy.HasValue)
            {
                switch(sortBy.Value)
                {
                    case 2:
                        products = products.OrderByDescending(x => x.Id).ToList();
                        break;
                    case 3:
                        products = products.OrderBy(x => x.Price).ToList();
                        break;
                    default:
                        products = products.OrderByDescending(x => x.Price).ToList();
                        break;
                }
            }
            return products.Skip((pageNo-1)*pageSize).Take(pageSize).ToList();
        }
        public int SearchProductsCount(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
        {
            var products = context.Products.ToList();
            if (categoryID.HasValue)
            {
                products = products.Where(x => x.Category.Id == categoryID.Value).ToList();
            }
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            }
            else
            {
                products = context.Products.ToList();
            }
            if (minimumPrice.HasValue)
            {
                products = products.Where(x => x.Price == minimumPrice.Value).ToList();
            }
            if (maximumPrice.HasValue)
            {
                products = products.Where(x => x.Price == maximumPrice.Value).ToList();
            }
            if (sortBy.HasValue)
            {
                switch (sortBy.Value)
                {
                    case 2:
                        products = products.OrderByDescending(x => x.Id).ToList();
                        break;
                    case 3:
                        products = products.OrderBy(x => x.Price).ToList();
                        break;
                    default:
                        products = products.OrderByDescending(x => x.Price).ToList();
                        break;
                }
            }
            return products.Count;
        }

    }
}
