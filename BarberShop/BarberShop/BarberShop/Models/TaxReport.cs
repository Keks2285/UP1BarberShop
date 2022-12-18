using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    public class TaxReport:INotifyPropertyChanged
    {
        public int ID_TaxReport { get; set; }

        public string Date_Report { get; set; }

        public string Date_Begin { get; set; }

        public string Date_End { get; set; }

        public double Value_Sells { get; set; }

        public double Value_Tax { get; set; }

        public int Employe_ID { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public EmployeModel employer { get; set; }
    }
}
