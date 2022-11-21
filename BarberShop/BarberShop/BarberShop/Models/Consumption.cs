using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    internal class Consumption
    {
        public int ID_Consumption { get; set; }

        public DateTime Date { get; set; }

        public double Value { get; set; }
    }
}
