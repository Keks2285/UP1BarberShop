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
        private static RestClient client = new RestClient("http://192.168.1.49:8080/BarberApi/");
        private ObservableCollection<EmployeModel> _employes= new ObservableCollection<EmployeModel>();
        private ObservableCollection<EmployeModel> _SearchEmployes = new ObservableCollection<EmployeModel>();
        //  private BindingList<PostEmploye> _posts;
        //  private BindingList<StatusEmploye> _status; 
        string fileName = "";
        public EmployersPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var req = new RestRequest("/getEmployers", Method.Get);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var res = client.Get(req);
            List<EmployeModel> data = JsonConvert.DeserializeObject<List<EmployeModel>>(res.Content);


            var reqPosts = new RestRequest("/getPosts", Method.Get);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var resPosts = client.Get(reqPosts);
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
            //EmployeModel.Posts = dataPosts;
            //EmployeModel.Posts

            foreach (var user in data)
            {
            
                _employes.Add(
                    new EmployeModel()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        MiddleName = user.MiddleName,
                        Email = user.Email,
                        INN = user.INN,
                        SelectedStatus = EmployeModel.Status[user.ID_Status-1],
                        SelectedPost = EmployeModel.Posts[user.ID_Post-1],
                    }
                );

            }



            //_employes = new BindingList<EmployeModel>()
            //{
            //    new EmployeModel(){
            //        FirstName="Новиков", 
            //        LastName="Илья", 
            //        MiddleName="Олеговмч",
            //        Email="example@gmail.com",
            //        INN="1234567890",
            //        SelectedStatus=EmployeModel.Status[1] }
            //};
            UsersGrid.ItemsSource = _employes;
           // ComboboxColumn.ItemsSource = _employes;
        }

        private void ImportEmploye_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".csv";
            openFileDialog.Filter = "CSV documents (.csv)|*.csv";
            if (openFileDialog.ShowDialog() == true)
            {
                fileName = openFileDialog.FileName;
                if (System.IO.Path.GetExtension(fileName) != ".csv") { MessageBox.Show("Неверный формат файла"); return; }                
            }
            var req = new RestRequest("/importEmploye", Method.Post);
            req.AddHeader("Content-Type", "multipart/form-data; charset=utf-8");
            req.AddFile("Employers", fileName);
            var res = client.Post(req);
            dynamic data = JsonConvert.DeserializeObject<dynamic>(res.Content);
            if (data.status.Value)
            {
                MessageBox.Show(data.message.Value); return;
            }
        }

        private void ExportEmploye_Click(object sender, RoutedEventArgs e)
        {
            try{
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
                if (saveFileDialog.ShowDialog() == true)
                    using (var writer  = new StreamWriter(saveFileDialog.FileName, false, Encoding.GetEncoding("utf-8")))
                    {
                        var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                        {
                            Delimiter = ";",
                        };
                        using (var csv= new CsvWriter(writer, csvConfig))
                        {
                            csv.WriteRecords(_employes);
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

            UsersGrid.ItemsSource = _employes.Where(
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
            UsersGrid.ItemsSource = _employes;
        }
    }
}
