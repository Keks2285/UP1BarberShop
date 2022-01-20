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
    /// Логика взаимодействия для Look_Employee.xaml
    /// </summary>
    public partial class Look_Employee : Window
    {
        public static SqlConnection connect = new SqlConnection(Connect.connectionString);
        string F = "";
        string I = "";
        string O = "";
        string EMAIL = "";
        string LOGIN = "";
        string PHONE = "";
        string SERIA = "";
        string NOMER = "";
        string POSTS = "";
        int ID = 0;
        int RANG = 0;
        public Look_Employee(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang, int id)
        {
            InitializeComponent();
            F = f;
            I = i;
            O = o;
            EMAIL = email;
            LOGIN = login;
            PHONE = phone;
            SERIA = seria;
            NOMER = nomer;
            POSTS = posts;
            RANG = rang;
            ID = id;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand command = new SqlCommand("SELECT ID_Employee, First_Name as 'Фамилия',Employee_Number as 'Телефон', Name_Employee as 'Имя', Middle_Name as 'Отчество', Seria_Pasporta as 'Серия паспорта', Nomer_Pasporta as 'Номер паспорта', Password_Employee as 'Пароль',Employee_Email as 'Почта', Login_Employee as 'Логин', Rang as 'Уровень доступа', Name_Status as 'Статус' from Employee join Status on FK_ID_Status=ID_Status", connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            // datatbl.Columns.AddRange();
            dg.ItemsSource = datatbl.DefaultView;
            dg.Columns[0].Visibility = Visibility.Hidden;
            dg.Columns[4].Visibility = Visibility.Hidden;
            dg.Columns[5].Visibility = Visibility.Hidden;
            dg.Columns[6].Visibility = Visibility.Hidden;
            connect.Close();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new BuhWindow(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
            this.Hide();
            haircuts.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
