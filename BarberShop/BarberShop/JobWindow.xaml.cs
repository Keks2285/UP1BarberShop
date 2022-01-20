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
    /// Логика взаимодействия для JobWindow.xaml
    /// </summary>
    public partial class JobWindow : Window
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
        int idEmployee = 0;
        int idPost = 0;
        int ID = 0;

        public JobWindow(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang, int id)
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

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Employe.SelectedValue!=null && Post.SelectedValue!=null)
            {
                connect.Open();
                SqlCommand add = new SqlCommand("Combination_Insert", connect);
                add.CommandType = CommandType.StoredProcedure;
                add.Parameters.AddWithValue("@FK_ID_Employee", Employe.SelectedValue);
                add.Parameters.AddWithValue("@FK_ID_Post", Post.SelectedValue);
                add.ExecuteNonQuery();
                connect.Close();
                Window_Loaded(sender, e);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (idEmployee != 0 && idPost != 0)
            {
                if (dg.SelectedItem == null) return;
                try
                {
                    connect.Open();
                    DataRowView row = (DataRowView)dg.SelectedItem;
                    SqlCommand Del = new SqlCommand("Combination_Delete", connect);
                    Del.CommandType = CommandType.StoredProcedure;
                    Del.Parameters.AddWithValue("ID_Combination", (int)row["ID_Combination"]);
                    Del.ExecuteNonQuery();
                }
               catch { MessageBox.Show("Что-то пошло не так :/"); }
                finally { 
                    connect.Close(); Window_Loaded(sender, e); idEmployee = 0;idPost = 0; 
                }
            }
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
            Window haircuts = new KadrOtdel(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
            this.Hide();
            haircuts.Show();
        }

        private void dg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dg.SelectedItem == null) return;
            DataRowView row = (DataRowView)dg.SelectedItem;
           idEmployee = Convert.ToInt32(row["ID_Combination"].ToString());
           idPost = Convert.ToInt32(row["ID_Post"].ToString());          
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand command = new SqlCommand("SELECT ID_Combination, ID_Employee, ID_Post, Name_Post as 'Должность', First_Name as 'Фамилия', Name_Employee as 'Имя', Middle_Name as 'Отчество'  from Employee join Combination on ID_Employee=FK_ID_Employee join Post  on ID_Post = FK_ID_Post", connect);
            DataTable datatbl = new DataTable();     
            datatbl.Load(command.ExecuteReader());
            // datatbl.Columns.AddRange();
            dg.ItemsSource = datatbl.DefaultView;
            dg.Columns[0].Visibility = Visibility.Hidden;
            dg.Columns[1].Visibility = Visibility.Hidden;
            dg.Columns[2].Visibility = Visibility.Hidden;




            SqlCommand commandEmployee = new SqlCommand("SELECT ID_Employee, concat(First_Name,' ', Name_Employee,' ' , Middle_Name) as 'ФИО'  from Employee", connect);
            DataTable datatblEmployee = new DataTable();
            datatblEmployee.Load(commandEmployee.ExecuteReader());
            Employe.ItemsSource = datatblEmployee.DefaultView;
            Employe.DisplayMemberPath = "ФИО";
            Employe.SelectedValuePath = "ID_Employee";


            SqlCommand commandPost = new SqlCommand("SELECT ID_Post, Name_Post as 'Должность'  from Post", connect);
            DataTable datatblPost = new DataTable();
            datatblPost.Load(commandPost.ExecuteReader());
            Post.ItemsSource = datatblPost.DefaultView;
            Post.DisplayMemberPath = "Должность";
            Post.SelectedValuePath = "ID_Post";
            connect.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
