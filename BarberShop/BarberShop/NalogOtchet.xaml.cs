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
    /// Логика взаимодействия для NalogOtchet.xaml
    /// </summary>
    public partial class NalogOtchet : Window
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
        public NalogOtchet(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang, int id)
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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (RANG >= 6)
            {
                Window admin = new Admin(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
                this.Hide();
                admin.Show();
                return;
            }
            Window haircuts = new BuhWindow(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
            this.Hide();
            haircuts.Show();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Dohod.Text != "" && Rashod.Text != "" && Date.Text!="")
            {
                try
                {
                    connect.Open();
                    SqlCommand add = new SqlCommand("Nalog_Otchet_Insert", connect);
                    add.CommandType = CommandType.StoredProcedure;
                    add.Parameters.AddWithValue("@Rashod", Rashod.Text);
                    add.Parameters.AddWithValue("@Dohod", Dohod.Text);
                    add.Parameters.AddWithValue("@Date_Otchet", Date.Text);
                    add.Parameters.AddWithValue("@FK_ID_Employee", ID);
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
                SqlCommand Del = new SqlCommand("Nalog_Otchet_Delete", connect);
                Del.CommandType = CommandType.StoredProcedure;
                Del.Parameters.AddWithValue("ID_Nalog_Otchet", (int)row["ID_Nalog_Otchet"]);
                Del.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Нельзя удалить Используемый склад"); }
            finally { connect.Close(); Window_Loaded(sender, e); Dohod.Text = ""; Rashod.Text = ""; Date.Text = ""; }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand commandcb1 = new SqlCommand("select ID_Nalog_Otchet, Rashod as 'Расходы', Dohod as 'Доходы', Date_Otchet as'Дата', concat(First_Name,' ',Name_Employee,' ', Middle_Name) as 'Сотрудник' from Nalog_Otchet join Employee on ID_Employee=FK_ID_Employee", connect);
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
            Dohod.Text = row["Доходы"].ToString().Replace(',','.');
            Rashod.Text = row["Расходы"].ToString().Replace(',', '.'); ;
            Date.Text = row["Дата"].ToString();      
        }

        private void Change_Click(object sender, RoutedEventArgs e)
        {
            if (Dohod.Text=="" || Rashod.Text == "" || Date.Text=="") { MessageBox.Show("Все поля должны быть заполненными"); return; }
            try
            {
                connect.Open();
                SqlCommand add = new SqlCommand("Nalog_Otchet_Update", connect);
                DataRowView row = (DataRowView)dg.SelectedItem;
                add.CommandType = CommandType.StoredProcedure;
                add.Parameters.AddWithValue("@ID_Nalog_Otchet", (int)row["ID_Nalog_Otchet"]);
                add.Parameters.AddWithValue("@Rashod", Rashod.Text);
                add.Parameters.AddWithValue("@Dohod", Dohod.Text);
                add.Parameters.AddWithValue("@Date_Otchet", Date.Text);
                add.Parameters.AddWithValue("@FK_ID_Employee", ID);
                add.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Введены некорректные данные"); }
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
