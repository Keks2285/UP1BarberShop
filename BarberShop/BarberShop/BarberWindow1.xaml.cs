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
    /// Логика взаимодействия для BarberWindow1.xaml
    /// </summary>
    public partial class BarberWindow1 : Window
    {
        string F = "";
        string I = "";
        string O = "";
        int RANG = 0;
        int ID = 0;
        public BarberWindow1(string login, string seria, string nomer, string email, string posts, string  f, string i, string o, string phone, int rang, int id)
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
            ID = id;
            RANG = rang;
            Hi.Content = $@"Здравствуйте {i} {o}";
        }

        private void HairCut_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new HairCut(Login.Content.ToString(), Seria.Content.ToString(), Nomer.Content.ToString(), Email.Content.ToString(), Posts.Content.ToString(), F, I,O, Phone.Content.ToString(), RANG, ID);
            this.Hide();
            haircuts.Show();
        }

        private void Uslugi_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new Uslugi(Login.Content.ToString(), Seria.Content.ToString(), Nomer.Content.ToString(), Email.Content.ToString(), Posts.Content.ToString(), F, I, O, Phone.Content.ToString(), RANG, ID);
            this.Hide();
            haircuts.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
