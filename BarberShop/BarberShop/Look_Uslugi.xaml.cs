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
    /// Логика взаимодействия для Look_Uslugi.xaml
    /// </summary>
   
    public partial class Look_Uslugi : Window
    {
        public static SqlConnection connect = new SqlConnection(Connect.connectionString);
        string F = "";
        string I = "";
        string O = "";
        string EMAIL = "";
        string LOGIN = "";
        string PASSWORD = "";
        int ID = 0;
        public Look_Uslugi(string login, string password, string name, string Lastname, string otch, int id)
        {
            InitializeComponent();
            connect.Open();
            SqlCommand commandcb1 = new SqlCommand("select Name_Serwice as 'Название', Price_Serwice 'Цена', Name_Instrument as 'Инструмент', Name_Haircut as 'Прическа' ,concat(First_Name,' ',Name_Employee,' ', Middle_Name) as 'Сотрудник' from Serwice join Employee on ID_Employee=FK_ID_Employee join Haircut on ID_Haircut=FK_ID_Haircut join Instrument on ID_Instrument = FK_ID_Instrument", connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(commandcb1.ExecuteReader());
            dg.ItemsSource = datatbl.DefaultView;
            connect.Close();
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
    }
}
