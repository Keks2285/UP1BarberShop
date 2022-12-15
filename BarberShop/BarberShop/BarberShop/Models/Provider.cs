using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BarberShop.Models
{
    public class Provider : INotifyPropertyChanged
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
        public static List<string> allNames = new List<string>();

        private string _name_provider;
        public string Name_Provider { 
            get
            {
                return _name_provider;
            }
            set
            {
                if (allNames.Contains(value))
                {
                    MessageBox.Show("Поставщик с тааким именем уже есть");
                    return;
                }
                allNames.Add(value);
                _name_provider = value;
                OnPropertyChanged("Name_Provider");
            }
        }

        private string _inn;

        public static List<string> AllINN = new List<string>();
        public string INN { 
            get
            {
                return _inn;
            }
            set
            {
                if (value == null || value == "")
                {
                    MessageBox.Show("ИНН не должен быть пустым");
                    return;
                }
                if (value.Length != 10)
                {
                    MessageBox.Show("ИНН должен содержать 10 цифр");
                    return;
                }
                if (AllINN.Contains(value))
                {
                    MessageBox.Show("ИНН должен быть уникальным");
                    return;
                }
                if (!Helper.INNcheck(value))
                {
                    MessageBox.Show("ИНН должен содержать только цифры");
                    return;
                }
                _inn = value;
                AllINN.Add(value);
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
