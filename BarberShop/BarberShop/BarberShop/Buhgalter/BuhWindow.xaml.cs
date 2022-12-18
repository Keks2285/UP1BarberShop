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
        int employer_id = 0;
        public BuhWindow(string _firstName, string _lastName, int id)
        {
            InitializeComponent();
            employer_id=id;
            MainFrame.Content = new TaxReports(id);
        }
        /// <summary>
        /// событеие нажатия кнопки открывающей страницу с отчетами
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new TaxReports(employer_id);
        }
        /// <summary>
        /// событеие закрытия окна
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
