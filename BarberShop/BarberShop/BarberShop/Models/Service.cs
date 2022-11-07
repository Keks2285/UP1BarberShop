using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace BarberShop.Models
{
    internal class Service
    {
        
        public int ID_Service { get; set;}

        public string Name_Service { get; set; }

        public double Price_Service { get; set; }
    }
}
