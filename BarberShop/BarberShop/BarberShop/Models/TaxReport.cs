using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    internal class TaxReport
    {
        public int ID_TaxReport { get; set; }

        public string DateReport { get; set; }
        public string Date_Begin { get; set; }
        public string Date_End { get; set; }

        public double ValueSells { get; set; }

        public double ValueTax { get; set; }

        public int Employe_ID { get; set; }
    }
}
