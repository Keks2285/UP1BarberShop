using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Xceed.Wpf.Toolkit;
using CsvHelper.Configuration.Attributes;

namespace BarberShop.Models
{
    public class Material:INotifyPropertyChanged
    {

        public int ID_Material { get; set; }

        public List<string>allNames= new List<string>();    

        private string _name_material;

        public string Name_Material {
            get { 
                return _name_material; 
            }
            set
            {
                if (allNames.Contains(value))
                {
                    MessageBox.Show("Материал уже существует");
                    return;
                }
                _name_material = value;
                OnPropertyChanged(Name_Material);
            }
        }

        public int Stock_ID { get; set; }

        public int Supply_ID { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static ObservableCollection<Stock> Stocks { get; set; } = new ObservableCollection<Stock>();
        public static ObservableCollection<Supply> Supplies { get; set; } = new ObservableCollection<Supply>();

        [Ignore]
        public Stock stock { get; set; }
        [Ignore]
        public Supply supply { get; set; }
    }
}
