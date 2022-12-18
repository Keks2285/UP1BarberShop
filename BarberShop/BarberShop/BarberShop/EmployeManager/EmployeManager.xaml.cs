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

namespace BarberShop.EmployeMAnager
{
    /// <summary>
    /// Логика взаимодействия для EmployeManager.xaml
    /// </summary>
    public partial class EmployeManager : Window
    {
        string firstName = "";
        string lastName = "";
        public EmployeManager( string _firstName, string _lastName)
        {
            lastName = _lastName;
            firstName =_firstName;
            InitializeComponent();
            loadFirstPage();
        }
        /// <summary>
        /// событеие загрузки окна
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FLTb.Text = firstName.Substring(0,1) + lastName.Substring(0, 1);
            
        }
        /// <summary>
        /// Метод загрузки первой страницы
        /// </summary>
        private void loadFirstPage()
        {
            EmployeBtn.Background = Brushes.Gray;
            PostBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            SickLeaveBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            //VacationBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            MainFrame.Content = new EmployeManagerPages.EmployersPage();
        }

        /// <summary>
        /// событеие нажатия кнопки открывающей страницу с сотрудниками
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void EmployeBtn_Click(object sender, RoutedEventArgs e)
        {
            loadFirstPage();
        }
        /// <summary>
        /// событеие нажатия кнопки открывающей страницу с должностями
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void PostBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            PostBtn.Background = Brushes.Gray;
            SickLeaveBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            //VacationBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            MainFrame.Content = new EmployeManagerPages.PostList();
        }
        /// <summary>
        /// событеие нажатия кнопки открывающей страницу с больничными и отпусками
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void SickLeaveBtn_Click(object sender, RoutedEventArgs e)
        {
            EmployeBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
            PostBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183)); ;
            SickLeaveBtn.Background = Brushes.Gray;

            MainFrame.Content = new EmployeManagerPages.VacationsPage();
            // VacationBtn.Background = new SolidColorBrush(Color.FromRgb(103, 58, 183));
        }


        ////// <summary>
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
