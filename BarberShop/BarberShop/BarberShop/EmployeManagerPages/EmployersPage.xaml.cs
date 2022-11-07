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
        
        private BindingList<EmployeModel> _employers= new BindingList<EmployeModel>();
        private BindingList<EmployeModel> employersBufer = new BindingList<EmployeModel>();
        //private ObservableCollection<EmployeModel> _SearchEmployes = new ObservableCollection<EmployeModel>();
        //  private BindingList<PostEmploye> _posts;
        // private BindingList<StatusEmploye> _status; 
        string fileName = "";
        public EmployersPage()
        {
            InitializeComponent();
            _employers.ListChanged += _employes_CollectionChanged;

            
        }


        
        private void _employes_CollectionChanged(object sender, ListChangedEventArgs e)
        {
        
           if (selectedEmployer == null) return;
           if (e.ListChangedType == ListChangedType.ItemDeleted)
           {
           }
           if (e.ListChangedType == ListChangedType.ItemChanged)
           {
               // MessageBox.Show(selectedEmployer.Email);
           }
        
        

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           
            var req = new RestRequest("/getEmployers", Method.Get);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var res = Helper.client.Get(req);
            List<EmployeModel> data = JsonConvert.DeserializeObject<List<EmployeModel>>(res.Content);

            if (EmployeModel.Posts.Count<1) { 
                var reqPosts = new RestRequest("/getPosts", Method.Get);
                req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resPosts = Helper.client.Get(reqPosts);
                List<PostEmploye> dataPosts = JsonConvert.DeserializeObject<List<PostEmploye>>(resPosts.Content);

                foreach (var post in dataPosts) {
                   EmployeModel.Posts.Add(
                        new PostEmploye()
                        {
                            Name = post.Name,
                            Price = post.Price,
                            Id = post.Id
                        }
                    );
                }
            }

            foreach (var user in data)
            {
            
                _employers.Add(
                    new EmployeModel()
                    {
                        ID_Employee = user.ID_Employee,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        MiddleName = user.MiddleName,
                        Password = user.Password,
                        Email = user.Email,
                        INN = user.INN,
                        ID_Post = user.ID_Post,
                        ID_Status = user.ID_Status,
                        SelectedStatus = EmployeModel.Status[user.ID_Status-1],
                        SelectedPost = EmployeModel.Posts[user.ID_Post-1],
                    }
                );

            }

            ///////////////////// статистика доходов за текущий год
            var reqIncomes = new RestRequest("/getIncomes", Method.Get);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resIncomes = Helper.client.Get(reqIncomes);
            List<Income> dataIncomes = JsonConvert.DeserializeObject<List<Income>>(resIncomes.Content);

            double[] months = new double[12];
            int counterMonth = 0;
            for (int i = 1; i<13;i++)
            {
                var curentMonth = from income in dataIncomes where 
                                  income.Date_Income.Month == i && income.Date_Income.Year== DateTime.Now.Year
                                  select income;
                foreach ( Income x in curentMonth)
                {
                    months[counterMonth]+=x.Value;
                };

                counterMonth++;

            }

            ///////////////


            UsersGrid.ItemsSource = _employers;
            double[] values = {1,2,1};
            double[] positions={0,1,2 };
            string [] labels = {"Работает", "В отпуске", "На больничном" };
            EmployersStatistic.Plot.AddBar(values, positions);
            EmployersStatistic.Plot.XTicks(positions, labels);
            EmployersStatistic.Plot.SetAxisLimits(yMin: 0);
            EmployersStatistic.Plot.XAxis.Grid(false);
            EmployersStatistic.Plot.SaveFig("stats_histogram.png");
            EmployersStatistic.Refresh();
            //employersBufer = _employers;

        }

        private void ImportEmploye_Click(object sender, RoutedEventArgs e)
        {
            try { 
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.DefaultExt = ".csv";
                openFileDialog.Filter = "CSV documents (.csv)|*.csv";
                if (openFileDialog.ShowDialog() == true)
                {
                    fileName = openFileDialog.FileName;
                    //MessageBox.Show( fileName);
                    if (System.IO.Path.GetExtension(fileName) != ".csv") { MessageBox.Show("Неверный формат файла"); return; }                
                }
                var req = new RestRequest("/importEmploye", Method.Post);
                req.AddHeader("Content-Type", "multipart/form-data;");
                req.AddFile("Employers", fileName);
                var res = Helper.client.Post(req);
                dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);
                if (data.status.Value)
                {
                    MessageBox.Show(data.message.Value); return;
                }
            } catch { }
            //employersBufer = _employers;
            Page_Loaded(sender, e);
        }

        private void ExportEmploye_Click(object sender, RoutedEventArgs e)
        {
            try{
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
                saveFileDialog.DefaultExt="csv";
                //saveFileDialog.GetType
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                if (saveFileDialog.ShowDialog() == true)
                    using (var writer  = new StreamWriter(saveFileDialog.FileName, false, Encoding.UTF8))
                    {
                       
                        var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                        {
                            Delimiter = ";",
                            HasHeaderRecord = false,
                        };
                        using (var csv= new CsvWriter(writer, csvConfig))
                        {
                            csv.WriteRecords(_employers);
                        }

                    }
                MessageBox.Show("Данные о сотрудниках успешно экспортированы");
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так, возможно файл уже используется другим процессом");
            }
           
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            //_SearchEmployes = new ObservableCollection<EmployeModel>(_employes.Where(
            //    item => item.FirstName.Contains(FirstNameTb.Text) &&
            //    item.LastName.Contains(LastNameTb.Text) &&
            //    item.MiddleName.Contains(MiddleNameTb.Text)
            //    ));
            // UsersGrid.ItemsSource =_SearchEmployes;
            UsersGrid.ItemsSource = _employers.Where(
                item => item.FirstName.Contains(FirstNameTb.Text) &&
                item.LastName.Contains(LastNameTb.Text) &&
                item.MiddleName.Contains(MiddleNameTb.Text)
                );
        }

        private void ClearSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            FirstNameTb.Text = "";
            LastNameTb.Text = "";
            MiddleNameTb.Text = "";
            UsersGrid.ItemsSource = _employers;
        }

        private void UsersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(UsersGrid.SelectedItem!=null)selectedEmployer = (EmployeModel)UsersGrid.SelectedItem;
        }

        private void UsersGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {




            switch (e.Column.Header.ToString())
            {
                case "ИНН":
                    {
                        if (sortAscINN) {
                            // _employers.
                            _employers=new BindingList<EmployeModel>(_employers.ToList().OrderBy(x => x.INN).ToList());
                            //_employers.ResetBindings();
                             UsersGrid.ItemsSource =  _employers;
                            _employers.ListChanged += _employes_CollectionChanged;
                            // UsersGrid.;
                            sortAscINN = false;
                        }
                        else
                        {
                            //_employers = _employers.OrderByDescending(x => x.INN).ToList();

                            _employers =new BindingList<EmployeModel>(_employers.ToList().OrderByDescending(x => x.INN).ToList());
                             UsersGrid.ItemsSource =  _employers;
                            _employers.ListChanged += _employes_CollectionChanged;
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
                            _employers.ListChanged += _employes_CollectionChanged;
                            // UsersGrid.;
                            sortAscEmail = false;
                        }
                        else
                        {
                            //_employers = _employers.OrderByDescending(x => x.INN).ToList();

                            
                            _employers = new BindingList<EmployeModel>(_employers.ToList().OrderByDescending(x => x.Email).ToList());
                          //  _employers.ResetBindings();
                            UsersGrid.ItemsSource = _employers;
                            _employers.ListChanged += _employes_CollectionChanged;
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
                            _employers.ListChanged += _employes_CollectionChanged;
                            // UsersGrid.;
                            sortAscFirstName = false;
                        }
                        else
                        {
                            //_employers = _employers.OrderByDescending(x => x.INN).ToList();


                            _employers = new BindingList<EmployeModel>(_employers.ToList().OrderByDescending(x => x.FirstName).ToList());
                            //  _employers.ResetBindings();
                            UsersGrid.ItemsSource = _employers;
                            _employers.ListChanged += _employes_CollectionChanged;
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
                            _employers.ListChanged += _employes_CollectionChanged;
                            // UsersGrid.;
                            sortAscLastName = false;
                        }
                        else
                        {
                            //_employers = _employers.OrderByDescending(x => x.INN).ToList();


                            _employers = new BindingList<EmployeModel>(_employers.ToList().OrderByDescending(x => x.LastName).ToList());
                            //  _employers.ResetBindings();
                            UsersGrid.ItemsSource = _employers;
                            _employers.ListChanged += _employes_CollectionChanged;
                            sortAscLastName = true;
                        }
                        break;
                    }

            }
           // MessageBox.Show( );
        }

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
    }
}
