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
    /// Логика взаимодействия для Satus.xaml
    /// </summary>
    public partial class Satus : Window
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
        int ID = 0;
        public Satus(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang ,int id)
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
            Window barber = new KadrOtdel(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
            this.Hide();
            barber.Show();
        }
        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg.SelectedItem;
           Status.Text = row["Название"].ToString();
            
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Status.Text.Length > 1)
            {
                try
                {
                    connect.Open();
                    SqlCommand add = new SqlCommand("Status_Insert", connect);
                    add.CommandType = CommandType.StoredProcedure;
                    add.Parameters.AddWithValue("@Name_Status", Status.Text);
                   
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
                if (Status.Text == "" ) { MessageBox.Show("Все поля должны быть заполненными"); return; }
                connect.Open();
                DataRowView row = (DataRowView)dg.SelectedItem;
                SqlCommand Upd = new SqlCommand("Status_Update", connect);
                Upd.CommandType = CommandType.StoredProcedure;
                Upd.Parameters.AddWithValue("@ID_Status", (int)row["ID_Status"]);
                Upd.Parameters.AddWithValue("@Name_Status", Status.Text);              
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
                SqlCommand Del = new SqlCommand("Status_Delete", connect);
                Del.CommandType = CommandType.StoredProcedure;
                Del.Parameters.AddWithValue("ID_Status", (int)row["ID_Status"]);
                Del.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Нельзя удалить используемый статус"); }
            finally { connect.Close(); Window_Loaded(sender, e); Status.Text = ""; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand commandcb1 = new SqlCommand("select ID_Status, Name_Status as 'Название' from Status", connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(commandcb1.ExecuteReader());
            dg.ItemsSource = datatbl.DefaultView;
            dg.Columns[0].Visibility = Visibility.Hidden;
            connect.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
