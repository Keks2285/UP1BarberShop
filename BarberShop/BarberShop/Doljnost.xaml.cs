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
    /// Логика взаимодействия для Doljnost.xaml
    /// </summary>
    public partial class Doljnost : Window
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
        public Doljnost(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang)
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Post.Text.Length > 1 && Oklad.Text.Length>1)
            {
                try
                {
                    connect.Open();
                    SqlCommand add = new SqlCommand("Post_Insert", connect);
                    add.CommandType = CommandType.StoredProcedure;
                    add.Parameters.AddWithValue("@Name_Post", Post.Text);
                    add.Parameters.AddWithValue("@Post_Price", Oklad.Text);
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
                if (Post.Text == "" || Oklad.Text=="") { MessageBox.Show("Все поля должны быть заполненными"); return; }
                connect.Open();
                DataRowView row = (DataRowView)dg.SelectedItem;
                SqlCommand Upd = new SqlCommand("Post_Update", connect);
                Upd.CommandType = CommandType.StoredProcedure;
                Upd.Parameters.AddWithValue("@ID_Post", (int)row["ID_Post"]);
                Upd.Parameters.AddWithValue("@Name_Post", Post.Text);
                Upd.Parameters.AddWithValue("@Post_Price", Oklad.Text);
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
                SqlCommand Del = new SqlCommand("Post_Delete", connect);
                Del.CommandType = CommandType.StoredProcedure;
                Del.Parameters.AddWithValue("ID_Post", (int)row["ID_Post"]);
                Del.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Нельзя удалить используемую должность"); }
            finally { connect.Close(); Window_Loaded(sender, e); Post.Text = ""; Oklad.Text = ""; }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window barber = new KadrOtdel(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG);
            this.Hide();
            barber.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand commandcb1 = new SqlCommand("select ID_Post, Name_Post as 'Название', Post_Price as 'оклад' from Post", connect);
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
            Post.Text = row["Название"].ToString();
            Oklad.Text = row["оклад"].ToString().Replace(',','.');
        }
    }
}
