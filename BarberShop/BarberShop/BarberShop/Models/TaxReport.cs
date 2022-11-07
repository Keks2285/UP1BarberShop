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

        string Date_Report { get; set; }

        string Date_End { get; set; }

        int Value_Sells { get; set; }

        int Value_Tax { get; set; }

        int Employe_ID { get; set; }
    }
}
