using BarberShop.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BarberShop.EmployeManagerPages
{
    /// <summary>
    /// Логика взаимодействия для PostsPages.xaml
    /// </summary>
    public partial class PostList : Page
    {
         //regex that matches disallowed text
        bool sortAscPrice= false;
        private BindingList<PostEmploye> _posts = new BindingList<PostEmploye>();
        private BindingList<PostEmploye> _searched_posts = new BindingList<PostEmploye>();
        PostEmploye selectedPost = new PostEmploye();
        public PostList()
        {
            InitializeComponent();
            _posts.ListChanged += _posts_CollectionChanged;
            //_searched_posts.ListChanged += _posts_CollectionChanged;


        }
        /// <summary>
        /// событеие изменения списка должностей
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void _posts_CollectionChanged(object sender, ListChangedEventArgs e)
        {
            try
            {
                if (e.ListChangedType == ListChangedType.ItemDeleted)
                {
                }
                if (e.ListChangedType == ListChangedType.ItemChanged)
                {
                    var req = new RestRequest("/updatePost", Method.Post);
                    PostEmploye post = (PostEmploye)PostDg.SelectedItem;
                    req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    req.AddParameter("id_post", post.Id);
                    req.AddParameter("namepost", post.Name);
                    req.AddParameter("price", post.Price);
                    var res = Helper.client.Post(req);
                    dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);
                    MessageBox.Show("Данные изменены");
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// событеие загрузки страницы
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var req = new RestRequest("/getPosts", Method.Get);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var res = Helper.client.Get(req);
            List<PostEmploye> data = JsonConvert.DeserializeObject<List<PostEmploye>>(res.Content);

            foreach (var post in data)
            {
                _posts.Add(post);
            }

            PostDg.ItemsSource = _posts;
        }
        /// <summary>
        /// событеие выбора должности
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void PostDg_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPost = (PostEmploye)PostDg.SelectedItem;
        }
        /// <summary>
        /// событеие возникающее перед вводом символа
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
        /// <summary>
        /// метод валидации
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static bool IsTextAllowed(string text)
        {
            Regex onlyNumbers = new Regex("[^0-9.-]+");
            return !onlyNumbers.IsMatch(text);
        }
        /// <summary>
        /// Событие сортировки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PostDg_Sorting(object sender, DataGridSortingEventArgs e)
        {
            




            switch (e.Column.Header.ToString())
            {
                case "Оклад":
                    {
                        if (sortAscPrice)
                        {
                            // _employers.
                            _posts = new BindingList<PostEmploye>(_posts.ToList().OrderBy(x => x.Price).ToList());
                            //_employers.ResetBindings();
                            PostDg.ItemsSource = _posts;
                           // _posts.ListChanged += _posts_CollectionChanged;
                            // UsersGrid.;
                            sortAscPrice = false;
                        }
                        else
                        {
                            //_employers = _employers.OrderByDescending(x => x.INN).ToList();

                            _posts = new BindingList<PostEmploye>(_posts.ToList().OrderByDescending(x => x.Price).ToList());
                            PostDg.ItemsSource = _posts;
                            //_posts.ListChanged += _posts_CollectionChanged;
                            sortAscPrice = true;
                        }
                        break;
                    }

            }
           // MessageBox.Show( );
        
        }
        /// <summary>
        /// Событие поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            //_searched_posts= (BindingList<PostEmploye>)_posts.Where(item => item.Name.Contains(NameTb.Text));
            _searched_posts.Clear();
            foreach (PostEmploye item in _posts.Where(item => item.Name.Contains(NameTb.Text)))
            {
                _searched_posts.Add(item);
            }
            PostDg.ItemsSource = null;
            PostDg.ItemsSource = _searched_posts;
        }
        /// <summary>
        /// Событие нажатие кнопки, создающей должность
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Creatpost_Click(object sender, RoutedEventArgs e)
        {
            var req = new RestRequest("/createPost", Method.Post);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            req.AddParameter("name_post", NameTb.Text);
            req.AddParameter("price", Price.Text);
            var res = Helper.client.Post(req);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);
            MessageBox.Show("Должность создана");
        }
        /// <summary>
        /// событие возникащее до нажатияна клаывишу
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void PostDg_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (selectedPost.Id <= 4)
                {
                    MessageBox.Show("Нельзя удалить системную должность");
                    _posts.Add(selectedPost);
                    return;
                }



                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                result = MessageBox.Show("Вы хотите удалить выбранную должность, это приведет к удалению\n всех связанных с ней сотрудников.\n Продолжить?", "Предупреждение", button, icon, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    var reqDeleteEmployer = new RestRequest("/removePost", Method.Post);
                    reqDeleteEmployer.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    reqDeleteEmployer.AddParameter("id", selectedPost.Id);
                    var resDeleteEmployer = Helper.client.Post(reqDeleteEmployer);
                    string a = resDeleteEmployer.Content;
                }
                else
                {
                    // _employers.AddNew();
                    _posts.Add(selectedPost);
                    return;
                }

            }
        }
    }
}
