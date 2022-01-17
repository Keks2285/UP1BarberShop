using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Echeiki.xaml
    /// </summary>
    public partial class Echeiki : Window
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
        public Echeiki()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Employe.SelectedValue != null && Post.SelectedValue != null)
            {
                connect.Open();
                SqlCommand add = new SqlCommand("Echeiki_Insert", connect);
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

        }
    }
}
