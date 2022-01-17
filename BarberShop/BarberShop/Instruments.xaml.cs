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
    /// Логика взаимодействия для Instruments.xaml
    /// </summary>
    public partial class Instruments : Window
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
        public Instruments(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang)
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand commandcb1 = new SqlCommand("select ID_Instrument, Name_Instrument as 'Название' from Instrument", connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(commandcb1.ExecuteReader());
            dg.ItemsSource = datatbl.DefaultView;
            dg.Columns[0].Visibility = Visibility.Hidden;
            connect.Close();
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg.SelectedItem;
            Instrument_Name.Text = row["Название"].ToString();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new Zakupmen(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG);
            this.Hide();
            haircuts.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Instrument_Name.Text.Length > 1 )
            {
                try
                {
                    connect.Open();
                    SqlCommand add = new SqlCommand("Instrument_Insert", connect);
                    add.CommandType = CommandType.StoredProcedure;
                    add.Parameters.AddWithValue("@Name_Instrument", Instrument_Name.Text);
                    add.ExecuteNonQuery();
                }
                catch { }
                finally
                {
                    connect.Close();
                    Window_Loaded(sender, e);
                }


            }
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg.SelectedItem == null) return;
                if (Instrument_Name.Text == "" ) { MessageBox.Show("Все поля должны быть заполненными"); return; }
                connect.Open();
                DataRowView row = (DataRowView)dg.SelectedItem;
                SqlCommand Upd = new SqlCommand("Instrument_Update", connect);
                Upd.CommandType = CommandType.StoredProcedure;
                Upd.Parameters.AddWithValue("@ID_Instrument", (int)row["Id_Instrument"]);
                Upd.Parameters.AddWithValue("@Name_Instrument", Instrument_Name.Text);
                Upd.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Введены некорректные данные"); }
            finally
            {
                connect.Close();
                Window_Loaded(sender, e);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (dg.SelectedItem == null) return;
            try
            {
                connect.Open();
                DataRowView row = (DataRowView)dg.SelectedItem;
                SqlCommand Del = new SqlCommand("Instrument_Delete", connect);
                Del.CommandType = CommandType.StoredProcedure;
                Del.Parameters.AddWithValue("ID_Instrument", (int)row["Id_Instrument"]);
                Del.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Нельзя удалить используемый инструмент"); }
            finally { connect.Close(); Window_Loaded(sender, e); Instrument_Name.Text = ""; }
        }
    }
}
