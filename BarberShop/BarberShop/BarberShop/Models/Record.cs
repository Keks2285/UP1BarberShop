using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    internal class Record
    {
        public int ID_Record { get; set; }
        public DateTime Date_Record { get; set; }
        public int Service_ID { get; set; }
        public int Client_ID { get; set; }

        
    }
}
