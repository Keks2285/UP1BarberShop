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
    /// Логика взаимодействия для Zakupki.xaml
    /// </summary>
    public partial class Zakupki : Window
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
        public Zakupki(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang, int id)
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
            Window haircuts = new Zakupmen(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
            this.Hide();
            haircuts.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Price.Text!=""&&Value.Text!="" && Instrument.SelectedValue != null)
            {
                try
                {
                    connect.Open();
                    SqlCommand add = new SqlCommand("Zakupka_Insert", connect);
                    add.CommandType = CommandType.StoredProcedure;
                    add.Parameters.AddWithValue("@Value_Instrument", Value.Text);
                    add.Parameters.AddWithValue("@Price_Zakupka", Price.Text);
                    add.Parameters.AddWithValue("@FK_ID_Instrument", Instrument.SelectedValue);
                    add.ExecuteNonQuery();
            }
                catch { MessageBox.Show("Введены некорректные данные"); }
            finally
            {
                connect.Close();
                    Window_Loaded(sender, e);
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

                if (dg.SelectedItem == null) return;
                try
                {
                    connect.Open();
                    DataRowView row = (DataRowView)dg.SelectedItem;
                    SqlCommand Del = new SqlCommand("Zakupka_Delete", connect);
                    Del.CommandType = CommandType.StoredProcedure;
                    Del.Parameters.AddWithValue("ID_Zakupka", (int)row["ID_Zakupka"]);
                    Del.ExecuteNonQuery();
                }
                catch { MessageBox.Show("Что-то пошло не так :/"); }
                finally
                {
                    connect.Close(); Window_Loaded(sender, e); EcheikaId = 0;
                }
            
        }
       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand command = new SqlCommand("select ID_Zakupka, FK_ID_Instrument, Name_Instrument as 'Инструмент', Value_Instrument as'Количество', Price_Zakupka as 'Цена закупки' from Zakupka join Instrument on FK_ID_Instrument = ID_Instrument", connect);
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
            connect.Close();
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg.SelectedItem;
            Value.Text = row["Количество"].ToString();
            Price.Text = row["Цена закупки"].ToString().Replace(',','.');
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (Value.Text=="" || Price.Text=="" || Instrument.SelectedValue == null) { MessageBox.Show("Все поля должны быть заполненными"); return; }
            try
            {
                connect.Open();
                SqlCommand add = new SqlCommand("Zakupka_Update", connect);
                DataRowView row = (DataRowView)dg.SelectedItem;
                add.CommandType = CommandType.StoredProcedure;
                add.Parameters.AddWithValue("@ID_Zakupka", (int)row["ID_Zakupka"]);
                add.Parameters.AddWithValue("@Value_Instrument", Value.Text);
                add.Parameters.AddWithValue("@Price_Zakupka", Price.Text);
                add.Parameters.AddWithValue("@FK_ID_Instrument", Instrument.SelectedValue);               
                add.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Введены некорректные данные или сотрудник с таким данными уже существует"); }
            finally
            {
                connect.Close();
                Window_Loaded(sender, e);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
