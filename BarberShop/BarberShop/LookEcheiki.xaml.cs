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
    /// Логика взаимодействия для LookEcheiki.xaml
    /// </summary>
    public partial class LookEcheiki : Window
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
        int RANG = 0;
        int EcheikaId = 0;
        int ID = 0;
        public LookEcheiki(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang, int id)
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
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new Zakupmen(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
            this.Hide();
            haircuts.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand command = new SqlCommand("select ID_Echeika, ID_Sklad, Name_Instrument as 'Инструмент', Adres_Sklad as 'Адрес'  from Instrument join Echeika on ID_Instrument=FK_ID_Instrument join Sklad on ID_Sklad=FK_ID_Sklad", connect);
            DataTable datatbl = new DataTable();
            dg.ItemsSource = datatbl.DefaultView;
            dg.Columns[0].Visibility = Visibility.Hidden;
            dg.Columns[1].Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
