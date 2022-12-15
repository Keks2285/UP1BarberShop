using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CsvHelper.Configuration.Attributes;

namespace BarberShop.Models
{
    public class PostEmploye : INotifyPropertyChanged
    {
        
        public int Id { get; set; }
        private string _name;
        public static List<string> AllNamePosts = new List<string>();
        public string Name { 
            get {return _name; }
            set
            {
                if (!Helper.CheckPostName(value))
                {
                    MessageBox.Show("Название должности должно состоять только из кириллицу");
                    return;
                }
                if (Id <= 4 && (value != "Менеджер отдела кадров" && value != "Бухгалтер" && value != "Начальник склада" && value != "Администратор БД"))
                {
                    MessageBox.Show("Нельзя поменять название системной должности, только оклад");
                    return;
                }


                _name = value;
                OnPropertyChanged("Name");
            } 
        }

        private double _price;
        public double Price {
            get
            {
                return _price;
            }
            set
            {
                if(value < 13000)
                {
                    MessageBox.Show("Оклад не может быть меньше МРОТ");
                    return;
                }
                _price = value;
                OnPropertyChanged("Price");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
