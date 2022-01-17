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
    /// Логика взаимодействия для SkladManager.xaml
    /// </summary>
    public partial class SkladManager : Window
    {
        string F = "";
        string I = "";
        string O = "";
        int RANG = 0;
        public SkladManager(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang)
        {
            InitializeComponent();
            Login.Content = login;
            Seria.Content = seria;
            Nomer.Content = nomer;
            Email.Content = email;
            Posts.Content = posts;
            Phone.Content = phone;
            F = f;
            I = i;
            O = o;
            RANG = rang;
            Hi.Content = $@"Здравствуйте {i} {o}";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Sklads_Click(object sender, RoutedEventArgs e)
        {
            Window manageSklad = new ManageSklad(Login.Content.ToString(), Seria.Content.ToString(), Nomer.Content.ToString(), Email.Content.ToString(), Posts.Content.ToString(), F, I, O, Phone.Content.ToString(), RANG);
            this.Hide();
            manageSklad.Show();
        }
    }
}
