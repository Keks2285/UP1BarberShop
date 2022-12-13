using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    internal class Vacation
    {
        public int ID_Vacation { get; set; }

        public string Date_Begin { get; set; }

        public string Date_End { get; set; }

        public int Employe_ID { get; set; }
        [Ignore]
        public string FIO { get; set; }

        [Ignore]
        public EmployeModel employer { get; set; }
    }
}
