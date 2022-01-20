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

namespace BarberShop
{
    /// <summary>
    /// Логика взаимодействия для UserWindow1.xaml
    /// </summary>
    public partial class UserWindow1 : Window
    {
        string LOGIN = "";
        string PASSWORD = "";
        string F = "";
        string I = "";
        string O = "";
        int ID = 0;
        public UserWindow1(string login, string password, string name, string lastname, string otch, int id)
        {
            InitializeComponent();
            LOGIN = login;
            PASSWORD = password;
            I = name;
            F = lastname;
            O = otch;
            ID = id;
        }

        private void ChangeData_Click(object sender, RoutedEventArgs e)
        {
            Window changeWindow = new ChangDataWindow(LOGIN, PASSWORD, I, F, O, ID);
            this.Hide();
            changeWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Log.Content = LOGIN;
            Hi.Content = $@"Здравствуйте  {I} {O}!";
            
        }

        private void Uslugi_Click(object sender, RoutedEventArgs e)
        {
            Window changeWindow = new Look_Uslugi(LOGIN, PASSWORD, I, F, O, ID);
            this.Hide();
            changeWindow.Show();
        }

        private void Haircut_Click(object sender, RoutedEventArgs e)
        {
            Window changeWindow = new Look_Haircut(LOGIN, PASSWORD, I, F, O, ID);
            this.Hide();
            changeWindow.Show();
        }

        private void Zapis_Click(object sender, RoutedEventArgs e)
        {
            Window changeWindow = new Zapis(LOGIN, PASSWORD, I, F, O, ID);
            this.Hide();
            changeWindow.Show();
        }
    }
}
