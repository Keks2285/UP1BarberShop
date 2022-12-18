using BarberShop.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Data;

namespace BarberShop.EmployeManagerPages
{
    /// <summary> 
    /// Логика взаимодействия для EmployersPage.xaml
    /// </summary>
    public partial class EmployersPage : Page
    {

        EmployeModel selectedEmployer;
        // private static RoutedEventArgs pageEventArgs = new RoutedEventArgs();
        bool sortAscINN = false;
        bool sortAscEmail = false;
        bool sortAscFirstName = false;
        bool sortAscLastName = false;

        private BindingList<EmployeModel> _employers = new BindingList<EmployeModel>();
        private BindingList<EmployeModel> _searchemployers = new BindingList<EmployeModel>();
        //private BindingList<EmployeModel> employersBufer = new BindingList<EmployeModel>();
        //private ObservableCollection<EmployeModel> _SearchEmployes = new ObservableCollection<EmployeModel>();
        //  private BindingList<PostEmploye> _posts;
        // private BindingList<StatusEmploye> _status; 
        string fileName = "";
        public EmployersPage()
        {
            InitializeComponent();

            _employers.ListChanged += _employes_CollectionChanged;


        }


        /// <summary>
        /// Событие выбора сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _employes_CollectionChanged(object sender, ListChangedEventArgs e)
        {
          //  if (UsersGrid.SelectedItem != null)
             //   Helper.ValidData((EmployeModel)UsersGrid.SelectedItem);
            if (selectedEmployer == null) return;
            if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
            }
            if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                EmployeModel employe = (EmployeModel)UsersGrid.SelectedItem;
                //employe = selectedEmployer;
                // MessageBox.Show(employe.Email);
                if (employe.MiddleName.Length < 3 && employe.MiddleName!="") {
                    MessageBox.Show("Отчество слишком короткое");
                    return;
                }
                if (employe.LastName.Length < 2)
                {
                    MessageBox.Show("Имя слишком короткое");
                    return;
                }
                if (employe.FirstName.Length < 3)
                {
                    MessageBox.Show("Фамилия слишком короткая");
                    return;
                }
                var req = new RestRequest("/updateEmploye", Method.Post);
                req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                req.AddParameter("firstname", employe.FirstName);
                req.AddParameter("lasttname", employe.LastName);
                req.AddParameter("middlename", employe.MiddleName);
                req.AddParameter("email", employe.Email);
                req.AddParameter("inn", employe.INN);
                req.AddParameter("post_id", employe.SelectedPost.Id);
                req.AddParameter("status_id", employe.SelectedStatus.Id);
                req.AddParameter("id_employer", employe.ID_Employee);
                var res = Helper.client.Post(req);
              //  dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);
                MessageBox.Show("Данные изменены");

            }



        }
        /// <summary>
        /// событеие загрузки страницы
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (EmployeModel.Posts.Count > 0) EmployeModel.Posts.Clear();


                var reqPosts = new RestRequest("/getPosts", Method.Get);
                reqPosts.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resPosts = Helper.client.Get(reqPosts);
                List<PostEmploye> dataPosts = JsonConvert.DeserializeObject<List<PostEmploye>>(resPosts.Content);

                foreach (var post in dataPosts)
                {
                    EmployeModel.Posts.Add(
                         new PostEmploye()
                         {
                             Id = post.Id,
                             Name = post.Name,
                             Price = post.Price
                             
                         }
                     );
                }
            
            EmployeModel.AllINN.Clear();
            EmployeModel.AllEmail.Clear();
            var req = new RestRequest("/getEmployers", Method.Get);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var res = Helper.client.Get(req);
            List<EmployeModel> data = JsonConvert.DeserializeObject<List<EmployeModel>>(res.Content);

            

            foreach (var user in data)
            {
                var employer = user;
                employer.SelectedStatus = EmployeModel.Status[user.ID_Status - 1];
                employer.SelectedPost = EmployeModel.Posts[user.ID_Post - 1];
                _employers.Add(employer);
            }

            


            UsersGrid.ItemsSource = _employers;
            double[] values = { 0, 0, 0, 0};
            double[] statuts = { 0, 1, 2, 3};
            for (int i = 0; i < 4; i++)
            {
                var Employers = from emloyers in data
                                  where
                                  emloyers.ID_Status== i+1
                                  select emloyers;
                foreach (EmployeModel employe in Employers)
                {
                    values[i]+=1;
                }

            }
            string[] labels = { "Работает", "Уволены", "В отпуске","На больничном" };
            EmployersStatistic.Plot.AddBar(values, statuts);
            EmployersStatistic.Plot.XTicks(statuts, labels);
            EmployersStatistic.Plot.SetAxisLimits(yMin: 0);
            EmployersStatistic.Plot.XAxis.Grid(false);
            EmployersStatistic.Plot.SaveFig("stats_histogram.png");
            EmployersStatistic.Refresh();


           // int cnt = UsersGrid.Items.Count;
          //  for (int i = 0; i < cnt; i++)
           // {
           //     UsersGrid.RowBackground
           // }


            // employersBufer = _employers;

        }
        /// <summary>
        /// Событие нажатие кнопки импорта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImportEmploye_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.DefaultExt = ".csv";
                openFileDialog.Filter = "CSV documents (.csv)|*.csv";
                if (openFileDialog.ShowDialog() == true)
                {
                    fileName = openFileDialog.FileName;
                    //MessageBox.Show( fileName);
                    if (System.IO.Path.GetExtension(fileName) != ".csv") { MessageBox.Show("Неверный формат файла"); return; }
                }
                else
                {
                    MessageBox.Show("импорт отменен"); return;
                }
                var req = new RestRequest("/importEmploye", Method.Post);
                req.AddHeader("Content-Type", "multipart/form-data;");
                req.AddFile("Employers", fileName);
                var res = Helper.client.Post(req);
                dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);
                if (data.status.Value)
                {
                    MessageBox.Show(data.message.Value);
                }
            }
            catch { }
            //employersBufer = _employers;
            Page_Loaded(sender, e);
        }
        /// <summary>
        /// Событие нажатие кнопки экспорта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportEmploye_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
                saveFileDialog.DefaultExt = "csv";
                //saveFileDialog.GetType
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                if (saveFileDialog.ShowDialog() == true)
                    using (var writer = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {

                        var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                        {
                            Delimiter = ";",
                            HasHeaderRecord = false,
                        };
                        using (var csv = new CsvWriter(writer, csvConfig))
                        {
                            csv.WriteRecords(_employers);
                        }
                        MessageBox.Show("Данные о сотрудниках успешно экспортированы");
                        return;
                    }
                MessageBox.Show("Экспорт отменен");
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так, возможно файл уже используется другим процессом");
            }

        }
        /// <summary>
        /// Событие нажатие кнопки поиска
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            //_SearchEmployes = new ObservableCollection<EmployeModel>(_employes.Where(
            //    item => item.FirstName.Contains(FirstNameTb.Text) &&
            //    item.LastName.Contains(LastNameTb.Text) &&
            //    item.MiddleName.Contains(MiddleNameTb.Text)
            //    ));
            //// UsersGrid.ItemsSource =_SearchEmployes;


            //UsersGrid.ItemsSource = _employers.Where(
            //    item => item.FirstName.Contains(FirstNameTb.Text) &&
            //    item.LastName.Contains(LastNameTb.Text) &&
            //    item.MiddleName.Contains(MiddleNameTb.Text)
            //    );


            foreach (EmployeModel item in _employers.Where(
                item => item.FirstName.Contains(FirstNameTb.Text) &&
                item.LastName.Contains(LastNameTb.Text) &&
                item.MiddleName.Contains(MiddleNameTb.Text)
                ))
            {
                _searchemployers.Add(item);
            }

            UsersGrid.ItemsSource = _searchemployers;

        }

        private void ClearSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            FirstNameTb.Text = "";
            LastNameTb.Text = "";
            MiddleNameTb.Text = "";
            UsersGrid.ItemsSource = _employers;
        }
        /// <summary>
        /// событеие выбора сотрудника
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void UsersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersGrid.SelectedItem != null) selectedEmployer = (EmployeModel)UsersGrid.SelectedItem;
        }
        /// <summary>
        /// событие сортировки
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void UsersGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {




            switch (e.Column.Header.ToString())
            {
                case "ИНН":
                    {
                        if (sortAscINN)
                        {
                            // _employers.
                            _employers = new BindingList<EmployeModel>(_employers.ToList().OrderBy(x => x.INN).ToList());
                            //_employers.ResetBindings();
                            UsersGrid.ItemsSource = _employers;
                            //_employers.ListChanged += _employes_CollectionChanged;
                            // UsersGrid.;
                            sortAscINN = false;
                        }
                        else
                        {
                            //_employers = _employers.OrderByDescending(x => x.INN).ToList();

                            _employers = new BindingList<EmployeModel>(_employers.ToList().OrderByDescending(x => x.INN).ToList());
                            UsersGrid.ItemsSource = _employers;
                           // _employers.ListChanged += _employes_CollectionChanged;
                            sortAscINN = true;
                        }
                        break;
                    }
                case "Почта":
                    {
                        if (sortAscEmail)
                        {
                            // _employers.
                            _employers = new BindingList<EmployeModel>(_employers.ToList().OrderBy(x => x.Email).ToList());
                            // _employers.ResetBindings();
                            UsersGrid.ItemsSource = _employers;
                           // _employers.ListChanged += _employes_CollectionChanged;
                            // UsersGrid.;
                            sortAscEmail = false;
                        }
                        else
                        {
                            //_employers = _employers.OrderByDescending(x => x.INN).ToList();


                            _employers = new BindingList<EmployeModel>(_employers.ToList().OrderByDescending(x => x.Email).ToList());
                            //  _employers.ResetBindings();
                            UsersGrid.ItemsSource = _employers;
                           // _employers.ListChanged += _employes_CollectionChanged;
                            sortAscEmail = true;
                        }
                        break;
                    }
                case "Фамилия":
                    {
                        if (sortAscFirstName)
                        {
                            // _employers.
                            _employers = new BindingList<EmployeModel>(_employers.ToList().OrderBy(x => x.FirstName).ToList());
                            // _employers.ResetBindings();
                            UsersGrid.ItemsSource = _employers;
                           // _employers.ListChanged += _employes_CollectionChanged;
                            // UsersGrid.;
                            sortAscFirstName = false;
                        }
                        else
                        {
                            //_employers = _employers.OrderByDescending(x => x.INN).ToList();


                            _employers = new BindingList<EmployeModel>(_employers.ToList().OrderByDescending(x => x.FirstName).ToList());
                            //  _employers.ResetBindings();
                            UsersGrid.ItemsSource = _employers;
                           // _employers.ListChanged += _employes_CollectionChanged;
                            sortAscFirstName = true;
                        }
                        break;
                    }
                case "Имя":
                    {
                        if (sortAscLastName)
                        {
                            // _employers.
                            _employers = new BindingList<EmployeModel>(_employers.ToList().OrderBy(x => x.LastName).ToList());
                            // _employers.ResetBindings();
                            UsersGrid.ItemsSource = _employers;
                           // _employers.ListChanged += _employes_CollectionChanged;
                            // UsersGrid.;
                            sortAscLastName = false;
                        }
                        else
                        {
                            //_employers = _employers.OrderByDescending(x => x.INN).ToList();


                            _employers = new BindingList<EmployeModel>(_employers.ToList().OrderByDescending(x => x.LastName).ToList());
                            //  _employers.ResetBindings();
                            UsersGrid.ItemsSource = _employers;
                           // _employers.ListChanged += _employes_CollectionChanged;
                            sortAscLastName = true;
                        }
                        break;
                    }

            }
            // MessageBox.Show( );
        }
        /// <summary>
        /// событие возникащее до нажатияна клаывишу
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет данные событий</param>
        private void UsersGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                MessageBoxButton button = MessageBoxButton.YesNoCancel;
                MessageBoxImage icon = MessageBoxImage.Warning;
                MessageBoxResult result;
                result = MessageBox.Show("Вы хотите удалить сотрудника, это приведет к удалению\n всех связанных с ним данных.\n Продолжить?", "Предупреждение", button, icon, MessageBoxResult.Yes);
                if (result == MessageBoxResult.Yes)
                {
                    var reqDeleteEmployer = new RestRequest("/removeEployerByEmail", Method.Post);
                    reqDeleteEmployer.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                    reqDeleteEmployer.AddParameter("email", selectedEmployer.Email);
                    var resDeleteEmployer = Helper.client.Post(reqDeleteEmployer);
                }
                else
                {
                    // _employers.AddNew();
                    _employers.Add(selectedEmployer);
                    return;
                }
            }
        }
        /// <summary>
        /// Событие нажатие кнопки, открывающей страниу=цу создания сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmployerAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EmployerCreate());
        }

    }
}
