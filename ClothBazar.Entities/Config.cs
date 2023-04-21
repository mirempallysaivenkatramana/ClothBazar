using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothBazar.Entities
{
    public class Config
    {
        [Key]
        public string Key { set; get; }
        public string Value { get; set; }
    }
}
