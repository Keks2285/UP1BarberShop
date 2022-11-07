using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
namespace BarberShop.Models
{
    internal class Client
    {
        public int ID_Client { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Middle_Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
