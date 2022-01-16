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
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Window
    {
        public static SqlConnection connect = new SqlConnection(Connect.connectionString);
        public Reg()
        {
            InitializeComponent();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text != "" || Password.Text != "" || Login.Text.Length > 5 || Password.Text.Length > 5)
            { 
            try
                {
                    connect.Open();
                    SqlCommand add = new SqlCommand("Users_Insert", connect);
                    add.CommandType = CommandType.StoredProcedure;
                    add.Parameters.AddWithValue("@Login_User", Login.Text);
                    add.Parameters.AddWithValue("@Password_User", Password.Text);
                    add.ExecuteNonQuery();
                }
                catch { MessageBox.Show("Введены некорректные данные");}
                finally
                {
                    connect.Close();
                    Window mw = new MainWindow();
                    this.Hide();
                    mw.Show();
                }
            }
        }


    }
}
