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

namespace BarberShop.Buhgalter
{
    /// <summary>
    /// Логика взаимодействия для BuhWindow.xaml
    /// </summary>
    public partial class BuhWindow : Window
    {
        public BuhWindow(string _firstName, string _lastName, int id)
        {
            InitializeComponent();
            MainFrame.Content = new TaxReports(id);
        }

    }
}
