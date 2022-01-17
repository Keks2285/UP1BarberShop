﻿using System;
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
    /// Логика взаимодействия для KadrOtdel.xaml
    /// </summary>
    public partial class KadrOtdel : Window
    {
        public static SqlConnection connect = new SqlConnection(Connect.connectionString);
        string F = "";
        string I = "";
        string O = "";
        int RANG = 0;
        public KadrOtdel(string login, string seria, string nomer, string email, string posts, string f, string i, string o, string phone, int rang)
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

        private void Dolj_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new Doljnost(Login.Content.ToString(), Seria.Content.ToString(), Nomer.Content.ToString(), Email.Content.ToString(), Posts.Content.ToString(), F, I, O, Phone.Content.ToString(), RANG);
            this.Hide();
            haircuts.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new JobWindow(Login.Content.ToString(), Seria.Content.ToString(), Nomer.Content.ToString(), Email.Content.ToString(), Posts.Content.ToString(), F, I, O, Phone.Content.ToString(), RANG);
            this.Hide();
            haircuts.Show();
        }

        private void Employeers_Click(object sender, RoutedEventArgs e)
        {
            Window haircuts = new Employeers(Login.Content.ToString(), Seria.Content.ToString(), Nomer.Content.ToString(), Email.Content.ToString(), Posts.Content.ToString(), F, I, O, Phone.Content.ToString(), RANG);
            this.Hide();
            haircuts.Show();
        }
    }
}
