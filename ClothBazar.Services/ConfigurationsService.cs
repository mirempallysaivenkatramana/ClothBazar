using ClothBazar.Database;
using ClothBazar.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Services
{
    public class ConfigurationsService
    {
        #region Singleton
        public static ConfigurationsService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConfigurationsService();
                }
                return instance;
            }
        }
        private static ConfigurationsService instance { get; set; } //private modefier because instance should not be created out side class
        private ConfigurationsService()
        {
        }
        #endregion
        CBContext context = new CBContext();
        public Config GetConfig(string Key)
        {
            return context.Configurations.Find(Key);
        }
    }
}
