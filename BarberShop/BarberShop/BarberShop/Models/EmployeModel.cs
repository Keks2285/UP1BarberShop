﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarberShop.Models
{
    class EmployeModel
    {
        public int Id { get; set; }

        public int ID_Status { get; set; }

        public int ID_Post { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }

        public string Email { get; set; }

        public string INN { get; set; }

        private StatusEmploye _selectedStatus;

        private PostEmploye _selectedPost;

        public static ObservableCollection<StatusEmploye> Status { get; set; } = new ObservableCollection<StatusEmploye>
        {   new StatusEmploye{Id=1, Name="Работает"},
            new StatusEmploye{Id=2, Name="Уволен"},
            new StatusEmploye{Id=3, Name="В отпуске"},
            new StatusEmploye{Id=4, Name="На больничном"}
        };
        public static ObservableCollection<PostEmploye> Posts { get; set; } = new ObservableCollection<PostEmploye>
        {   new PostEmploye{Id=1, Name="Менеджер отдела кадров", Price=45000},
            new PostEmploye{Id=2, Name="Бухгалтер", Price=45000},
            new PostEmploye{Id=3, Name="Начальник склада", Price=45000}
        };


        public StatusEmploye SelectedStatus {
            get
            {
                return _selectedStatus;
            }
            set
            {
                _selectedStatus = value;

            } 
        }

        public PostEmploye SelectedPost
        {
            get
            {
                return _selectedPost;
            }
            set
            {
                _selectedPost = value;

            }
        }


    }
}