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

namespace BarberShop.ClientPages
{
    /// <summary>
    /// Логика взаимодействия для EmployeManager.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        string firstName = "";
        string lastName = "";
        int idClient = 0;
        public ClientWindow( string _firstName, string _lastName, int clientId)
        {
            idClient=clientId;
            firstName = _firstName;
            lastName = _lastName;
            InitializeComponent();
            loadFirstPage();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FLTb.Text = firstName.Substring(0,1) + lastName.Substring(0, 1);
            
        }

        private void loadFirstPage()
        {
            MainFrame.Content = new RecordServicePage(idClient);
        }


        private void EmployeBtn_Click(object sender, RoutedEventArgs e)
        {
            loadFirstPage();
        }

        private void PostBtn_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SickLeaveBtn_Click(object sender, RoutedEventArgs e)
        {
        }

       

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
