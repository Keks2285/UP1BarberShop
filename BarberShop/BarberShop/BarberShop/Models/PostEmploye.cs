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
        [Ignore]
        public int Id { get; set; }
        [Name("Post")]
        public string Name { get; set; }
        [Ignore]
        public double Price { get; set; }
    }
}
