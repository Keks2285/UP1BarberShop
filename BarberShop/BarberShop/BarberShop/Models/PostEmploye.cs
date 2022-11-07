using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace BarberShop.Models
{
     class PostEmploye
    {
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public double Price { get; set; }
    }
}
