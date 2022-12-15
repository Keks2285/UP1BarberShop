using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Windows;

namespace BarberShop.Models 
{
    public class EmployeModel : INotifyPropertyChanged
    {
        
        public int ID_Employee { get; set; }
        [Ignore]
        private string _firstName { get; set; }

        public string FirstName { get { return _firstName; }
            set
            {
                if (value.Length < 4)
                {
                    if (value == null || value == "")
                    {
                        MessageBox.Show("Фамилия не может быть пустой");
                        return;
                    }
                    MessageBox.Show("Фамилия слишком короткая ");
                    return;
                }
                if (!Helper.CheckFIO(value))
                {
                    MessageBox.Show("Фамилия должна содержать только кирилицу");
                    return;
                }
                _firstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        [Ignore]
        private string _lastName { get; set; }

        public string LastName {
            get { return _lastName; }
            set
            {
                if(value.Length<2)
                {
                    if (value == null || value == "")
                    {
                        MessageBox.Show("Имя не может быть пустым");
                        return;
                    }
                    MessageBox.Show("Имя слишком короткое ");
                    return;
                }
                
                if (!Helper.CheckFIO(value))
                {
                    MessageBox.Show("Имя должно содержать только кирилицу");
                    return;
                }
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }

        [Ignore]
        private string? _middleName { get; set; }

        public string? MiddleName {
            get { return _middleName; }
            set {
                if(value == null||value=="")
                {
                    _middleName = value;
                    OnPropertyChanged("MiddleName");
                    return;
                }
                if (value.Length < 2)
                {
                    MessageBox.Show("Отчество слишком короткое");
                    return;
                }
                if (!Helper.CheckFIO(value))
                {
                    MessageBox.Show("Отчество должно содержать только кирилицу");
                    return ;
                }
                _middleName = value;
                OnPropertyChanged("MiddleName");
            } 
        }

        [Ignore]

        private string _email;
        public static List<string> AllEmail = new List<string>();
        public string Email 
        {
            get { return _email; }
            set {
                if (!Helper.CheckEmail(value))
                {
                    MessageBox.Show("Неверный формат почты");
                    return;
                }
                if (AllEmail.Contains(value))
                {
                    MessageBox.Show("Почта должна быть уникальной");
                    return;
                }
                if(_email != null && AllEmail.Contains(_email))
                    AllEmail.Remove(_email);
                AllEmail.Add(value);
                _email = value;
                OnPropertyChanged("Email");
            } 
        }

        public string Password { get; set; }

        [Ignore]
        private string _inn;
        public static List<string> AllINN = new List<string>();
        public string INN {
            get { return _inn; }
            set
            {
                if(value == null || value == "")
                {
                    MessageBox.Show("ИНН не должен быть пустым");
                    return;
                }
                if (value.Length!=10 )
                {
                    MessageBox.Show("ИНН должен содержать 10 цифр");
                    return;
                }
                if (AllINN.Contains(value))
                {
                    MessageBox.Show("ИНН должен быть уникальным");
                    return;
                }
                if (!Helper.INNcheck(value)){
                    MessageBox.Show("ИНН должен содержать только цифры");
                    return;
                }
                if(_inn != null && AllINN.Contains(_inn))
                    AllINN.Remove(_inn);
                AllINN.Add(value);
                _inn = value;
                OnPropertyChanged("INN");
            } 
        }
       
        private int _id_post { get; set; }
        
        public int ID_Post{get; set;}
       
        private int _id_status { get; set; }

        public int ID_Status { get; set; }

        private StatusEmploye _selectedStatus;
        
        private PostEmploye _selectedPost;
       
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public static ObservableCollection<StatusEmploye> Status { get; set; } = new ObservableCollection<StatusEmploye>
        {  
            new StatusEmploye{Id=1, Name="Работает"},
            new StatusEmploye{Id=2, Name="Уволен"},
            new StatusEmploye{Id=3, Name="В отпуске"},
            new StatusEmploye{Id=4, Name="На больничном"}
        };
        public static ObservableCollection<PostEmploye> Posts { get; set; } = new ObservableCollection<PostEmploye>();

        [Ignore]
        public StatusEmploye SelectedStatus {
            get
            {
                return _selectedStatus;
            }
            set
            {
                if (_selectedStatus == value) return;
                //if (value.Name == "Уволен")
                //{
                //    MessageBox.Show(_selectedStatus.Name);
                //    MessageBox.Show(SelectedStatus.Name);
                //    return;
                //}
                _selectedStatus = value;
                ID_Status = value.Id;
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
                ID_Post = value.Id;
                OnPropertyChanged("selectedPost");

            }
        }

        
    }
}
