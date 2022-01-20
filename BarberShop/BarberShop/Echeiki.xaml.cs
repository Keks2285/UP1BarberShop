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
    /// Логика взаимодействия для Echeiki.xaml
    /// </summary>
    public partial class Echeiki : Window
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
        public Echeiki(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang, int id)
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
            ID = id;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (RANG >= 6)
            {
                Window admin = new Admin(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
                this.Hide();
                admin.Show();
                return;
            }
            Window haircuts = new SkladManager(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
            this.Hide();
            haircuts.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Sklad.SelectedValue != null && Instrument.SelectedValue != null)
            {
                connect.Open();
                SqlCommand add = new SqlCommand("Echeika_Insert", connect);
                add.CommandType = CommandType.StoredProcedure;
                add.Parameters.AddWithValue("@FK_ID_Sklad", Sklad.SelectedValue) ;
                add.Parameters.AddWithValue("@FK_ID_Instrument", Instrument.SelectedValue);
                add.ExecuteNonQuery();
                connect.Close();
                Window_Loaded(sender, e);
            }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (EcheikaId!=0)
            {
                if (dg.SelectedItem == null) return;
                try
                {
                    connect.Open();
                    DataRowView row = (DataRowView)dg.SelectedItem;
                    SqlCommand Del = new SqlCommand("Echeika_Delete", connect);
                    Del.CommandType = CommandType.StoredProcedure;
                    Del.Parameters.AddWithValue("ID_Echeika", (int)row["ID_Echeika"]);
                    Del.ExecuteNonQuery();
                }
                catch { MessageBox.Show("Что-то пошло не так :/"); }
                finally
                {
                    connect.Close(); Window_Loaded(sender, e); EcheikaId = 0;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand command = new SqlCommand("select ID_Echeika, ID_Sklad, Name_Instrument as 'Инструмент', Adres_Sklad as 'Адрес'  from Instrument join Echeika on ID_Instrument=FK_ID_Instrument join Sklad on ID_Sklad=FK_ID_Sklad", connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            // datatbl.Columns.AddRange();
            dg.ItemsSource = datatbl.DefaultView;
            dg.Columns[0].Visibility = Visibility.Hidden;
            dg.Columns[1].Visibility = Visibility.Hidden;


            SqlCommand commandEmployee = new SqlCommand("SELECT ID_Instrument, Name_Instrument as 'Инструмент'  from Instrument", connect);
            DataTable datatblEmployee = new DataTable();
            datatblEmployee.Load(commandEmployee.ExecuteReader());
            Instrument.ItemsSource = datatblEmployee.DefaultView;
            Instrument.DisplayMemberPath = "Инструмент";
            Instrument.SelectedValuePath = "ID_Instrument";

            SqlCommand commandSklad = new SqlCommand("SELECT ID_Sklad, Adres_Sklad as 'Склад'  from Sklad", connect);
            DataTable datatblSklad= new DataTable();
            datatblSklad.Load(commandSklad.ExecuteReader());
            Sklad.ItemsSource = datatblSklad.DefaultView;
            Sklad.DisplayMemberPath = "Склад";
            Sklad.SelectedValuePath = "ID_Sklad";
            connect.Close();
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg.SelectedItem;
             EcheikaId = Convert.ToInt32(row["ID_Echeika"].ToString());          
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
