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

namespace BarberShop.EmployeManagerPages
{
    /// <summary>
    /// Логика взаимодействия для EmployersPage.xaml
    /// </summary>
    public partial class EmployersPage : Page
    {
        private static RestClient client = new RestClient("http://192.168.1.49:8080/BarberApi/");
        private BindingList<EmployeModel> _employes= new BindingList<EmployeModel>();
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


            //var reqposts = new RestRequest("/getposts", Method.Get);
            //req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //var resposts = client.Get(reqposts);
            //ObservableCollection<PostEmploye> dataposts = JsonConvert.DeserializeObject<ObservableCollection<PostEmploye>>(resposts.Content);
            //EmployeModel.Posts = dataposts;


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
    }
}
