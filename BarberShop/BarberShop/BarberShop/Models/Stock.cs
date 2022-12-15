using CsvHelper.Configuration.Attributes;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models 
{
    public class Stock : INotifyPropertyChanged
    {
        
        public int ID_Stock { get; set; }

        private string _adres;
        public string Adres {
            get { return _adres; }
            set
            {
                _adres = value;
                OnPropertyChanged("Adres");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
