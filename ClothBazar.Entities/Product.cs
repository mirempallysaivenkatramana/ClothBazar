﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class Product:BaseEntity
    {
       // public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        [Range(1,100000)]
        public decimal Price { get; set; }
       
        public string ImageURL { get; set; }

    }
}
