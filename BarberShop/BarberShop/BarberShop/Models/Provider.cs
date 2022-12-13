using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    internal class Provider : INotifyPropertyChanged
    {
        public int ID_Provider { get; set; }

        private string _adres;
        public string Adres { 
            get
            {
                return _adres;
            }
            set
            {
                _adres = value;
                OnPropertyChanged("Adres");
            } 
        }
        private string _name_provider;
        public string Name_Provider { 
            get
            {
                return _name_provider;
            }
            set
            {
                _name_provider = value;
                OnPropertyChanged("Name_Provider");
            }
        }

        private string _inn;

        public static List<string> AllInn = new List<string>();
        public string INN { 
            get
            {
                return _inn;
            }
            set
            {
                _inn = value;
                AllInn.Add(value);
                OnPropertyChanged("INN");
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
