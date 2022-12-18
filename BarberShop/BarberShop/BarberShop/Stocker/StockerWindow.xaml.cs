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
        /// <summary>
        /// Фамилия авторизованного сотрудника
        /// </summary>
        string firstName = "";
        /// <summary>
        /// имя авторизованного сотрудника
        /// </summary>
        string lastName = "";
        public StockerWindow(string _firstName, string _lastName)
        {
            firstName = _firstName;
            lastName = _lastName;
            InitializeComponent();
            loadFirstPage();
        }


        /// <summary>
        /// метод загрузки первой страницы
        /// </summary>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>

        private void loadFirstPage()
        {
            ProvidersBtn.Background = Brushes.Gray;
            SuppliesBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            //VacationBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            MainFrame.Content = new ProvidersPage();
        }

        /// <summary>
        /// событеие обработки закрытия окна
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }
        /// <summary>
        /// событеие обработки перехода на страницу поставщиков и складов
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void ProvidersBtn_Click(object sender, RoutedEventArgs e)
        {
            loadFirstPage();
        }
        /// <summary>
        /// событеие обработки перехода на страницу поставок
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void SuppliesBtn_Click(object sender, RoutedEventArgs e)
        {
            ProvidersBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            SuppliesBtn.Background = Brushes.Gray;
            //VacationBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            MainFrame.Content = new SuppliesPage();
        }
        /// <summary>
        /// событеие обработки перехода на страницу материалов
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void MaterialsBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new MaterialsPage();
        }
    }
}
