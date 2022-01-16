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
        public SkladManager()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Sklads_Click(object sender, RoutedEventArgs e)
        {
            Window manageSklad = new ManageSklad();
            this.Hide();
            manageSklad.Show();
        }
    }
}
