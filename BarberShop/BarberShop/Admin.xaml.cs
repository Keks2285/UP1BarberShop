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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
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

        public Admin(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang, int id)
        {
            InitializeComponent();
            F = f;
            I = i;
            O = o;
            ID = id;
            RANG = rang;
        }

        private void Haircut_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new HairCut(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
            this.Hide();
            haircuts.Show();
        }

        private void Status_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new Satus(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG,ID);
            this.Hide();
            haircuts.Show();
        }

        private void Sklads_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new ManageSklad(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG,ID);
            this.Hide();
            haircuts.Show();
        }

        private void Echeiki_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new Echeiki(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG,ID);
            this.Hide();
            haircuts.Show();
        }

        private void Services_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new Uslugi(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
            this.Hide();
            haircuts.Show();
        }

        private void Otchets_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new NalogOtchet(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
            this.Hide();
            haircuts.Show();
        }

        private void Zakupki_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new Zakupki(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
            this.Hide();
            haircuts.Show();
        }

        private void Instruments_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new Instruments(LOGIN, SERIA, NOMER, EMAIL, POSTS, F, I, O, PHONE, RANG, ID);
            this.Hide();
            haircuts.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
