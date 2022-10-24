using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace BarberShop.Models
{
     class StatusEmploye
    {
        [Ignore]
        public int Id { get; set; }
        [Name("Status")]
        public string Name { get; set; }
    }
}
