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
    /// Логика взаимодействия для ChangDataWindow.xaml
    /// </summary>
    public partial class ChangDataWindow : Window
    {
        public static SqlConnection connect = new SqlConnection(Connect.connectionString);
        string LOGIN = "";
        string PASSWORD = "";
        string f = "";
        string i = "";
        string o = "";
        int id = 0;
        public ChangDataWindow(string login, string password, string name, string lastname, string otch, int ID)
        {
            InitializeComponent();
            Login.Text = login;
            Password.Text = password;
            I.Text = name;
            F.Text = lastname;
            O.Text = otch;
            LOGIN = login;
            PASSWORD = password;
            i = name;
            f = lastname;
            o = otch;
            id = ID;
        }

        private void Change_Data_Click(object sender, RoutedEventArgs e)
        {
            connect.Open();
            SqlCommand c = new SqlCommand($@"select * from Users where Login_User='{Login.Text}' and ID_User!={id} ", connect);
            SqlDataReader reader = c.ExecuteReader();
            //reader.Close();
            if (reader.HasRows)  // если есть данные
            {
                MessageBox.Show("Логин уже занят, введите другой");
                //connect.Close();
            }
             else if (Login.Text.Length > 5 || Password.Text.Length > 5)
             {
                // connect.Open();
                reader.Close();
                SqlCommand Upd = new SqlCommand("Users_Update", connect);
                Upd.CommandType = CommandType.StoredProcedure;
                Upd.Parameters.AddWithValue("@ID_User", id);
                Upd.Parameters.AddWithValue("@Login_User", Login.Text);
                Upd.Parameters.AddWithValue("@Password_User", Password.Text);
                Upd.Parameters.AddWithValue("@LastName_User", F.Text);
                Upd.Parameters.AddWithValue("@Name_User", I.Text);
                Upd.Parameters.AddWithValue("@Otch_User", O.Text);
                Upd.ExecuteNonQuery();
                connect.Close();
            }
            
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Window u1 = new UserWindow1(LOGIN, PASSWORD, I.Text, F.Text, O.Text, id);
            this.Hide();
            u1.Show();
        }
    }
}
