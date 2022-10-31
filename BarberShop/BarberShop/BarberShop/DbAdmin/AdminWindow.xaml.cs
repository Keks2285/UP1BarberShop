using BarberShop.Models;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Win32;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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

namespace BarberShop.DbAdmin
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private static RestClient client = new RestClient("http://192.168.1.49:8080/BarberApi/");
        public AdminWindow()
        {
            InitializeComponent();
            var directories = Directory.GetDirectories(Helper.backupsDir);
            foreach (var dir in directories)
            {
                BackupsList.Items.Add(new DirectoryInfo(dir).Name);
            }
            
        }

        private void BackupsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {   
            //Удаление элементов ссписка
            //if(BackupsList.SelectedValue!=null)
            MessageBox.Show(BackupsList.SelectedValue.ToString());
            //BackupsList.Items.Remove(BackupsList.SelectedItem);
        }

        private void CreatePoint_Click(object sender, RoutedEventArgs e)
        {
            if (!Directory.Exists(Helper.backupsDir))
                Directory.CreateDirectory(Helper.backupsDir);
            




            BindingList<EmployeModel> _employers = new BindingList<EmployeModel>();
            var req = new RestRequest("/getEmployers", Method.Get);
            req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            var res = client.Get(req);
            List<EmployeModel> data = JsonConvert.DeserializeObject<List<EmployeModel>>(res.Content);


            if (EmployeModel.Posts.Count < 1)
            {
                var reqPosts = new RestRequest("/getPosts", Method.Get);
                req.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                var resPosts = client.Get(reqPosts);
                List<PostEmploye> dataPosts = JsonConvert.DeserializeObject<List<PostEmploye>>(resPosts.Content);

                foreach (var post in dataPosts)
                {
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
                        SelectedStatus = EmployeModel.Status[user.ID_Status - 1],
                        SelectedPost = EmployeModel.Posts[user.ID_Post - 1],
                    }
                );

            }

            try
            {
                Directory.CreateDirectory(Helper.backupsDir+DateTime.Now.ToString().Replace(":",""));
                using (var writer = new StreamWriter(Helper.backupsDir+DateTime.Now.ToString().Replace(":","")+"\\Employe.csv", false, Encoding.GetEncoding("utf-8")))
                    {
                        var csvConfig = new CsvConfiguration(CultureInfo.GetCultureInfo("ru-RU"))
                        {
                            Delimiter = ";",
                        };
                        using (var csv = new CsvWriter(writer, csvConfig))
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
    }
}
