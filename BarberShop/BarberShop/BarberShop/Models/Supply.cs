using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    internal class Supply
    {

        public int ID_Supply { get; set; }

        public DateOnly Date_Supply { get; set; }

        public int Value { get; set; }

        public int Provider_ID { get; set; }

    }
}
