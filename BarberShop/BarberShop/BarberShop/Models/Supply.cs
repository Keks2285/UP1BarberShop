using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.Toolkit;

namespace BarberShop.Models
{
    public class Supply : INotifyPropertyChanged
    {

        public int ID_Supply { get; set; }

        private string _date_supply;

        public string Date_Supply {
            get { return _date_supply; }
            set
            {
                if (!Helper.CheckDate(value)){
                    MessageBox.Show("Неверный формат даты");
                    return;
                }
                
                _date_supply = DateOnly.Parse(value).ToString("yyyy-MM-dd");
                OnPropertyChanged("Date_Supply");
            }
        }
        private int _value;

        public int Value {
            get { return _value; }
            set
            {
                if (value <= 0)
                {
                    MessageBox.Show("Объем не может быть отрицательным или равен 0");
                    return;
                }
                _value = value;
                OnPropertyChanged("Value");
            } 
        }

        public int Provider_ID { get; set; }

        [Ignore]
        public static ObservableCollection<Provider> Providers { get; set; } = new ObservableCollection<Provider>();
        [Ignore]
        public Provider selectedProvider { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
       
    
    }
}
