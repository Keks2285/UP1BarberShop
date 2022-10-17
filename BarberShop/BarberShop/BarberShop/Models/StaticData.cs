using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    internal class StaticData
    {
        new List<StatusEmploye> _status = new List<StatusEmploye>()
        {
                new StatusEmploye() { Name = "Работает"},
                new StatusEmploye() { Name = "Уволен"},
                new StatusEmploye() { Name = "В отпуске"},
                new StatusEmploye() { Name = "Уволен"}
        };
    }
}
