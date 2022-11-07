using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;

namespace BarberShop.Models 
{
    class EmployeModel : INotifyPropertyChanged
    {
        
        public int ID_Employee { get; set; }       

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string INN { get; set; }

        public int ID_Post { get; set; }

        public int ID_Status { get; set; }

        private StatusEmploye _selectedStatus;
        
        private PostEmploye _selectedPost;
       
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public static ObservableCollection<StatusEmploye> Status { get; set; } = new ObservableCollection<StatusEmploye>
        {   new StatusEmploye{Id=1, Name="Работает"},
            new StatusEmploye{Id=2, Name="Уволен"},
            new StatusEmploye{Id=3, Name="В отпуске"},
            new StatusEmploye{Id=4, Name="На больничном"}
        };
        public static ObservableCollection<PostEmploye> Posts { get; set; } = new ObservableCollection<PostEmploye>
        {  
        };

        [Ignore]
        public StatusEmploye SelectedStatus {
            get
            {
                return _selectedStatus;
            }
            set
            {
                if (_selectedStatus == value) return;
                _selectedStatus = value;
                OnPropertyChanged("selectedStatus");

            } 
        }
        [Ignore]
        public PostEmploye SelectedPost
        {
            get
            {
                return _selectedPost;
            }
            set
            {

                if (_selectedPost == value) return;
                _selectedPost = value;
                OnPropertyChanged("selectedPost");

            }
        }


    }
}
