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
    /// Логика взаимодействия для ManageSklad.xaml
    /// </summary>
    public partial class ManageSklad : Window
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
        int ID=0;
        public ManageSklad(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang, int id)
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
            Window a = new SkladManager(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG,ID);
            this.Hide();
            a.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Adress.Text.Length > 1 && Value.Text!="")
            {
                try
                {
                    connect.Open();
                    SqlCommand add = new SqlCommand("Sklad_Insert", connect);
                    add.CommandType = CommandType.StoredProcedure;
                    add.Parameters.AddWithValue("@Adres_Sklad", Adress.Text);
                    add.Parameters.AddWithValue("@Value_Echeek", Value.Text);
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

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dg.SelectedItem == null) return;
                if (Adress.Text == "" || Value.Text == "") { MessageBox.Show("Все поля должны быть заполненными"); return; }
                connect.Open();
                DataRowView row = (DataRowView)dg.SelectedItem;
                SqlCommand Upd = new SqlCommand("Sklad_Update", connect);
                Upd.CommandType = CommandType.StoredProcedure;
                Upd.Parameters.AddWithValue("@ID_Sklad", (int)row["ID_Sklad"]);
                Upd.Parameters.AddWithValue("@Adres_Sklad", Adress.Text);
                Upd.Parameters.AddWithValue("@Value_Echeek", Value.Text);
                Upd.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Введены некорректные данные"); 
            }
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
                SqlCommand Del = new SqlCommand("Sklad_Delete", connect);
                Del.CommandType = CommandType.StoredProcedure;
                Del.Parameters.AddWithValue("ID_Sklad", (int)row["ID_Sklad"]);
                Del.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Нельзя удалить Используемый склад"); }
            finally { connect.Close(); Window_Loaded(sender, e); Adress.Text = ""; Value.Text = "";}
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg.SelectedItem;
            Adress.Text = row["Адресс"].ToString();
           Value.Text = row["Количество ячеек"].ToString();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand commandcb1 = new SqlCommand("select ID_Sklad, Adres_Sklad as 'Адресс', Value_Echeek as 'количество ячеек' from Sklad", connect);
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
