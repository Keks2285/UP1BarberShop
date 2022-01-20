using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Look_Haircut.xaml
    /// </summary>
    public partial class Look_Haircut : Window
    {
        public static SqlConnection connect = new SqlConnection(Connect.connectionString);
        string F = "";
        string I = "";
        string O = "";
        string EMAIL = "";
        string LOGIN = "";
        string PASSWORD = "";
        int ID = 0;
        public Look_Haircut(string login, string password, string name, string Lastname, string otch, int id)
        {
            InitializeComponent();
            F = Lastname;
            I = name;
            O = otch;
            PASSWORD = password;
            LOGIN = login;
            ID = id;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window barber = new UserWindow1(LOGIN, PASSWORD, I, F, O, ID);
            this.Hide();
            barber.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand commandcb1 = new SqlCommand("select ID_Haircut, Name_Haircut as 'Название' from Haircut", connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(commandcb1.ExecuteReader());
            dg.ItemsSource = datatbl.DefaultView;
            dg.Columns[0].Visibility = Visibility.Hidden;
            connect.Close();
        }
    }
}
