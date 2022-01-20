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
    /// Логика взаимодействия для Uslugi.xaml
    /// </summary>
    public partial class Uslugi : Window
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
        public Uslugi(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang, int id)
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
            SqlCommand commandcb1 = new SqlCommand("select ID_Serwice, Name_Serwice as 'Название', Price_Serwice 'Цена', Name_Instrument as 'Инструмент', Name_Haircut as 'Прическа' ,concat(First_Name,' ',Name_Employee,' ', Middle_Name) as 'Сотрудник' from Serwice join Employee on ID_Employee=FK_ID_Employee join Haircut on ID_Haircut=FK_ID_Haircut join Instrument on ID_Instrument = FK_ID_Instrument", connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(commandcb1.ExecuteReader());
            dg.ItemsSource = datatbl.DefaultView;
            dg.Columns[0].Visibility = Visibility.Hidden;


            SqlCommand commandEmployee = new SqlCommand("SELECT ID_Instrument, Name_Instrument as 'Инструмент'  from Instrument", connect);
            DataTable datatblEmployee = new DataTable();
            datatblEmployee.Load(commandEmployee.ExecuteReader());
            Instrument.ItemsSource = datatblEmployee.DefaultView;
            Instrument.DisplayMemberPath = "Инструмент";
            Instrument.SelectedValuePath = "ID_Instrument";

            SqlCommand commandHaircut = new SqlCommand("SELECT ID_Haircut, Name_Haircut as 'Прическа'  from Haircut", connect);
            DataTable datatblHaircut = new DataTable();
            datatblHaircut.Load(commandHaircut.ExecuteReader());
            Haircut.ItemsSource = datatblHaircut.DefaultView;
            Haircut.DisplayMemberPath = "Прическа";
            Haircut.SelectedValuePath = "ID_Haircut";
            connect.Close();        
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
            Window barber = new BarberWindow1(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
            this.Hide();
            barber.Show();
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (Usluga.Text != "" && Price.Text != "" && Instrument.SelectedValue != null)
            {
                try
                {
                    connect.Open();
                    SqlCommand add = new SqlCommand("Serwice_Update", connect);
                    DataRowView row = (DataRowView)dg.SelectedItem;
                    add.CommandType = CommandType.StoredProcedure;
                    add.Parameters.AddWithValue("@ID_Serwice",  (int)row["ID_Serwice"]);
                    add.Parameters.AddWithValue("@Name_Serwice", Usluga.Text);
                    add.Parameters.AddWithValue("@Price_Serwice", Price.Text);
                    add.Parameters.AddWithValue("@FK_ID_Employee", ID);
                    add.Parameters.AddWithValue("@FK_ID_Instrument", Instrument.SelectedValue);
                    add.Parameters.AddWithValue("@FK_ID_Haircut", Haircut.SelectedValue);
                    add.ExecuteNonQuery();
                    connect.Close();
                    Window_Loaded(sender, e);
                }
                catch { MessageBox.Show("Введены некорректные данные"); }
                finally
                {
                    connect.Close();
                    Window_Loaded(sender, e);
                }
            }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Usluga.Text != "" && Price.Text != "" && Instrument.SelectedValue!=null)
            {
                try
                {
                    connect.Open();
                    SqlCommand add = new SqlCommand("Serwice_Insert", connect);
                    add.CommandType = CommandType.StoredProcedure;
                    add.Parameters.AddWithValue("@Name_Serwice", Usluga.Text);
                    add.Parameters.AddWithValue("@Price_Serwice", Price.Text);
                    add.Parameters.AddWithValue("@FK_ID_Employee", ID);
                    add.Parameters.AddWithValue("@FK_ID_Instrument", Instrument.SelectedValue);
                    add.Parameters.AddWithValue("@FK_ID_Haircut", Haircut.SelectedValue);
                    add.ExecuteNonQuery();
                    connect.Close();
                    Window_Loaded(sender, e);
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
                SqlCommand Del = new SqlCommand("Serwice_Delete", connect);
                Del.CommandType = CommandType.StoredProcedure;
                Del.Parameters.AddWithValue("ID_Serwice", (int)row["ID_Serwice"]);
                Del.ExecuteNonQuery();
            connect.Close();
            }
            catch { }
            finally { connect.Close(); Window_Loaded(sender, e); Haircut.Text = ""; }
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg.SelectedItem;
            Usluga.Text = row["Название"].ToString();
            Price.Text = row["Цена"].ToString().Replace(',','.');
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
