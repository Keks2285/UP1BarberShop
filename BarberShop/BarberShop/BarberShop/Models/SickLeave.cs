﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    internal class SickLeave
    {
        public int ID_SickLeave { get; set; }

        public string Date_Begin { get; set; }

        public string Date_End { get; set; }

        public int Employe_ID { get; set; }
    }
}