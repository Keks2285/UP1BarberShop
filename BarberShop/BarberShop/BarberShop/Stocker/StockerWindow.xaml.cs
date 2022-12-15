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

namespace BarberShop.Stocker
{
    /// <summary>
    /// Логика взаимодействия для StockerWindow.xaml
    /// </summary>
    public partial class StockerWindow : Window
    {
        string firstName = "";
        string lastName = "";
        public StockerWindow(string _firstName, string _lastName)
        {
            firstName = _firstName;
            lastName = _lastName;
            InitializeComponent();
            loadFirstPage();
        }




        private void loadFirstPage()
        {
            ProvidersBtn.Background = Brushes.Gray;
            SuppliesBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            //VacationBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            MainFrame.Content = new ProvidersPage();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void ProvidersBtn_Click(object sender, RoutedEventArgs e)
        {
            loadFirstPage();
        }

        private void SuppliesBtn_Click(object sender, RoutedEventArgs e)
        {
            ProvidersBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            SuppliesBtn.Background = Brushes.Gray;
            //VacationBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            MainFrame.Content = new SuppliesPage();
        }

        private void MaterialsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new MaterialsPage();
        }
    }
}
