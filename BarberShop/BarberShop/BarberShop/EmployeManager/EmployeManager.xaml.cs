using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BarberShop.EmployeMAnager
{
    /// <summary>
    /// Логика взаимодействия для EmployeManager.xaml
    /// </summary>
    public partial class EmployeManager : Window
    {
        string firstName = "";
        string lastName = "";
        public EmployeManager( string _firstName, string _lastName)
        {
            lastName = _lastName;
            firstName =_firstName;
            InitializeComponent();
            loadFirstPage();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FLTb.Text = firstName.Substring(0,1) + lastName.Substring(0, 1);
            
        }

        private void loadFirstPage()
        {
            EmployeBtn.Background = Brushes.Gray;
            PostBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            SickLeaveBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            VacationBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            MainFrame.Content = new EmployeManagerPages.EmployersPage();
        }


        private void EmployeBtn_Click(object sender, RoutedEventArgs e)
        {
            loadFirstPage();
        }

        private void PostBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            PostBtn.Background = Brushes.Gray;
            SickLeaveBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            VacationBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            MainFrame.Content = new EmployeManagerPages.PostList();
        }

        private void SickLeaveBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            PostBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183)); ;
            SickLeaveBtn.Background = Brushes.Gray;
            VacationBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
        }

        private void VacationBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            PostBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            SickLeaveBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            VacationBtn.Background = Brushes.Gray;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
